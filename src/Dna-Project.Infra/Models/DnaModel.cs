namespace Dna_Project.Infra.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class DnaModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Dna { get; set; }

        public bool IsMutant { get; set; }
    }
}
