FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /Source
COPY ["FeedbackReporting.Presentation.csproj", ""]
RUN dotnet restore "./FeedbackReporting.Presentation.csproj"
COPY . .
WORKDIR "/Source/."
RUN dotnet build "FeedbackReporting.Presentation.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "FeedbackReporting.Presentation.csproj" -v Release -o /app/publish
FROM base AS final
WORDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FeedbackReporting.Presentation.dll"]
