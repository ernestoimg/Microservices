#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Servicios.API.Libreria/Servicios.API.Libreria.csproj", "Servicios.API.Libreria/"]
COPY ["Services.ContextMongoDb/Services.ContextMongoDb.csproj", "Services.ContextMongoDb/"]
COPY ["Services.Entities/Services.Entities.csproj", "Services.Entities/"]
COPY ["Services.Configurations/Services.Configurations.csproj", "Services.Configurations/"]
COPY ["Services.Business/Services.Business.csproj", "Services.Business/"]
RUN dotnet restore "Servicios.API.Libreria/Servicios.API.Libreria.csproj"
COPY . .
WORKDIR "/src/Servicios.API.Libreria"
RUN dotnet build "Servicios.API.Libreria.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Servicios.API.Libreria.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Servicios.API.Libreria.dll"]