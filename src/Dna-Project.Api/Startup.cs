namespace Dna_Project.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Core.Services;
    using System;
    using System.Reflection;
    using System.Linq;
    using System.IO;
    using Dna_Project.Api.Middlewares;
    using Dna_Project.Infra.Interface;
    using Dna_Project.Infra.Repositories;
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos;
    using Dna_Project.Core.Interfaces.Services;
    using Dna_Project.Core.Interfaces.Strategies;
    using Dna_Project.Core.Strategies;
    using Dna_Project.Core.Strategies.DnaDirections;
    using Dna_Project.Core.Config;
    using Microsoft.Extensions.Options;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddCors();

            // Swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dna_Project.Api", Version = "v1" });

                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();

                Array.ForEach(xmlDocs, (d) =>
                {
                    c.IncludeXmlComments(d);
                });
            });

            // Services
            services.AddScoped<IMutantService, MutantService>();

            // Repositories
            services.AddSingleton<IMutantRepository>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());

            // Strategies
            services.AddScoped<IDnaStrategy, DnaStrategy>();
            services.AddScoped<IDnaDirection, RDnaDirection>();
            services.AddScoped<IDnaDirection, DDnaDirection>();
            services.AddScoped<IDnaDirection, DIDnaDirection>();
            services.AddScoped<IDnaDirection, DIRDnaDirection>();

            // Global variables
            Action<DnaConfig> mduOptions = (opt =>
            {
                opt.MinEquals = 2;
                opt.TotalToValidate = 4;
                opt.Letters = new char[] { 'A', 'G', 'T', 'C' };
            });
            services.Configure(mduOptions);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<DnaConfig>>().Value);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dna_Project.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // <InitializeCosmosClientInstanceAsync>        
        /// <summary>
        /// Creates a Cosmos DB database and a container with the specified partition key. 
        /// </summary>
        /// <returns></returns>
        private static async Task<MutantRepository> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;

            CosmosClient client = new(account, key);
            MutantRepository cosmosDbService = new(client, databaseName, containerName);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);

            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return cosmosDbService;
        }
    }
}
