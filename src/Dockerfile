FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Dna-Project.Api/Dna-Project.Api.csproj", "Dna-Project.Api/"]
RUN dotnet restore "Dna-Project.Api/Dna-Project.Api.csproj"
COPY . .
WORKDIR "/src/Dna-Project.Api"
RUN dotnet build "Dna-Project.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dna-Project.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dna-Project.Api.dll"]