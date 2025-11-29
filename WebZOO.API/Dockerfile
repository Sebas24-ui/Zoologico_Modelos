FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia TODOS los archivos y carpetas del repositorio a /src
# Esto es CRUCIAL para que 'dotnet restore' encuentre AMBOS proyectos
COPY . .

# Restaura las dependencias de toda la solución.
# El comando busca el proyecto principal (WebZOO.API.csproj) y sus dependencias (Zoologico_Modelos)
RUN dotnet restore "./WebZOO.API/WebZOO.API.csproj"

# Construye el proyecto principal
WORKDIR "/src/WebZOO.API"
RUN dotnet build "./WebZOO.API.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

# -----------------------------------------------------------
# ETAPA 2: PUBLICAR
# -----------------------------------------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/WebZOO.API"
RUN dotnet publish "./WebZOO.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# -----------------------------------------------------------
# ETAPA 3: FINAL (Ejecución)
# -----------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Puertos
EXPOSE 8080 
EXPOSE 8081

# Punto de entrada de la aplicación
ENTRYPOINT ["dotnet", "WebZOO.API.dll"]