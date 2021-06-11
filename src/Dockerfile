FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Dna-Project.Api/Dna-Project.Api.csproj"
WORKDIR "/src/Dna-Project.Api"
RUN dotnet build "Dna-Project.Api.csproj" -c Release -o /app

FROM build AS publish
WORKDIR "/src/Dna-Project.Api"
RUN dotnet publish "Dna-Project.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Dna-Project.Api.dll", "--server.urls", "http://0.0.0.0:5000"]