# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official ASP.NET Core build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TaskManagementSystem/TaskManagementSystem.csproj", "TaskManagementSystem/"]
RUN dotnet restore "TaskManagementSystem/TaskManagementSystem.csproj"
COPY . .
WORKDIR "/src/TaskManagementSystem"
RUN dotnet build "TaskManagementSystem.csproj" -c Release -o /app/build

# Build the project
FROM build AS publish
RUN dotnet publish "TaskManagementSystem.csproj" -c Release -o /app/publish

# Use the runtime image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManagementSystem.dll"]
