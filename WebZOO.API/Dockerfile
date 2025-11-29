FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# **CRÍTICO:** Copia TODOS los archivos de la solución a la raíz del contenedor.
# Esto asegura que ambos proyectos .csproj sean visibles.
COPY . .

# Restaura las dependencias de toda la solución.
# La ruta es relativa a la raíz del repositorio, donde están las carpetas.
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