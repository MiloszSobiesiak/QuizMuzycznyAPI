# Etap build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Etap runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV PORT=8080
EXPOSE 8080

CMD ["dotnet", "QuizMuzycznyAPI.dll"]