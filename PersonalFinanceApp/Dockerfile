# Use the official .NET 8.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj file and restore the dependencies
COPY ["PersonalFinanceApp/PersonalFinanceApp.csproj", "PersonalFinanceApp/"]
RUN dotnet restore "PersonalFinanceApp/PersonalFinanceApp.csproj"

# Copy the rest of the application code and build the app
COPY . .
WORKDIR "/src/PersonalFinanceApp"
RUN dotnet build "PersonalFinanceApp.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "PersonalFinanceApp.csproj" -c Release -o /app/publish

# Use the official ASP.NET Core runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "PersonalFinanceApp.dll"]
