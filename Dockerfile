FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
EXPOSE 80

# copy sln and csproj files
COPY FeedbackReporting.sln .
COPY Source/FeedbackReporting.Presentation/FeedbackReporting.Presentation.csproj ./Source/FeedbackReporting.Presentation/
COPY Source/FeedbackReporting.Application/FeedbackReporting.Application.csproj ./Source/FeedbackReporting.Application/
COPY Source/FeedbackReporting.Domain/FeedbackReporting.Domain.csproj ./Source/FeedbackReporting.Domain/
COPY Utils/InMemoryDatabase/InMemoryDatabase.csproj ./Utils/InMemoryDatabase/
RUN dotnet restore Source/FeedbackReporting.Presentation/FeedbackReporting.Presentation.csproj

# copy source code
COPY Source/. ./Source/.
COPY Utils/. ./Utils/.

# build solution
WORKDIR /src/Source/FeedbackReporting.Presentation
RUN dotnet publish FeedbackReporting.Presentation.csproj -c Release -o /app --no-restore

# create final image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "FeedbackReporting.Presentation.dll", "--environment=Development", "0.0.0.0:80"]