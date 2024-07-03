# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project files
COPY ./AgriWeatherTracker/API/AgriWeatherTracker.csproj ./AgriWeatherTracker/API/
COPY ./AgriWeatherTracker/ ./AgriWeatherTracker/

# Restore the dependencies
RUN dotnet restore ./AgriWeatherTracker/API/AgriWeatherTracker.csproj

# Build the application
RUN dotnet build ./AgriWeatherTracker/API/AgriWeatherTracker.csproj -c Release -o /app/build

# Publish the application
RUN dotnet publish ./AgriWeatherTracker/API/AgriWeatherTracker.csproj -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published application files
COPY --from=build /app/publish .

# Expose port 80
EXPOSE 80

# Expose port 443
EXPOSE 443

# Set the entry point
ENTRYPOINT ["dotnet", "AgriWeatherTracker.dll"]
