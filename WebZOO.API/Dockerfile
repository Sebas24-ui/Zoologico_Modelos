FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# **CRUCIAL:** Copia TODOS los archivos y carpetas del repositorio a /src
# (Esto asegura que ambos proyectos .csproj sean visibles para dotnet restore)
COPY . .

# Restaura las dependencias de toda la solución
# El comando busca el proyecto principal y sus dependencias (Zoologico_Modelos)
RUN dotnet restore "WebZOO.API/WebZOO.API.csproj"

# Publica el proyecto principal
RUN dotnet publish "WebZOO.API/WebZOO.API.csproj" -c Release -o /app/publish --no-restore

# -----------------------------------------------------------
# ETAPA 2: FINAL (Ejecución)
# -----------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "WebZOO.API.dll"]