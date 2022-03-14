FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /Source

COPY *.sln /
COPY FeedbackReporting.Presnetation/*.csproj ./feedbackreporting/
RUN dotnet restore

COPY FeedbackReporting.Presentation/. ./feedbackreporting.
WORKDIR /source/feedbackreporting
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app .:
ENTRYPOINT ["dotnet", "FeedbackReporting.Presentation.dll"]