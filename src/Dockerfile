# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY *.sln .
COPY WebApi/*.csproj "./WebApi/"
COPY Application/*.csproj "./Application/"
COPY Domain/*.csproj "./Domain/"
COPY Infrastructure/*.csproj "./Infrastructure/"
COPY . .
RUN dotnet restore "./WebApi/RedArbor.WebApi.csproj"



# Copy the rest of the application code
RUN dotnet build "./WebApi/RedArbor.WebApi.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "./WebApi/RedArbor.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
# Stage 3: Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedArbor.WebApi.dll"]