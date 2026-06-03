FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Job Application Tracker/Job Application Tracker.csproj", "Job Application Tracker/"]
RUN dotnet restore "Job Application Tracker/Job Application Tracker.csproj"
COPY . .
WORKDIR "/src/Job Application Tracker"
RUN dotnet build "Job Application Tracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Job Application Tracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Job Application Tracker.dll"]