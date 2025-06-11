# 1. Osnovna slika .NET za izvajanje aplikacije
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# 2. Gradnja aplikacije
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY . .
WORKDIR "/src"
RUN dotnet build "TrgovinaElektronika.csproj" -c Release -o /app/build

# 3. Publikacija
FROM build AS publish
RUN dotnet publish "TrgovinaElektronika.csproj" -c Release -o /app/publish

# 4. Končna slika za izvajanje
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrgovinaElektronika.dll"]
