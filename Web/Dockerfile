# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the project file to the container
COPY ["Web.csproj", "./"]

# Restore the project dependencies
RUN dotnet restore "Web.csproj"

# Copy the rest of the application files to the container
COPY . .

# Build the application in Release mode and output to /app/build
RUN dotnet build "Web.csproj" -c Release -o /app/build

# Use the build stage to publish the application
FROM build AS publish

# Publish the application in Release mode to /app/publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish

# Use the ASP.NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS final

# Set the working directory inside the container
WORKDIR /app

# Copy the published application from the publish stage
COPY --from=publish /app/publish .

# Set the environment variable to configure the application to listen on port 80
ENV ASPNETCORE_URLS=http://+:80

# Expose port 80 for the application
EXPOSE 80

# Define the entry point for the container to run the application
ENTRYPOINT ["dotnet", "Web.dll"]
