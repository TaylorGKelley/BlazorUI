# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Set the working directory inside the container
WORKDIR /src

# copy the project files (use the exact filenames in your repo)
COPY ["BlazorUI.sln", "./"]
COPY ["UI/BlazorUI.csproj", "UI/"]
COPY ["Web/Web.csproj", "Web/"]

# Restore the project dependencies
RUN dotnet restore "BlazorUI.sln"

# copy the rest and build...
COPY . .
WORKDIR /src/Web
RUN dotnet publish -c Release -o /app/publish

# Use the ASP.NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5153
EXPOSE 5153

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]
