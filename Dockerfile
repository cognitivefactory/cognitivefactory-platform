FROM microsoft/dotnet:2.2-sdk-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY CognitiveFactory.Platform.API/*.csproj ./CognitiveFactory.Platform.API/
RUN dotnet restore

# copy everything else and build app
COPY CognitiveFactory.Platform.API/. ./CognitiveFactory.Platform.API/
WORKDIR /app/CognitiveFactory.Platform.API
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS runtime
WORKDIR /app
COPY --from=build /app/CognitiveFactory.Platform.API/out ./
ENTRYPOINT ["dotnet", "CognitiveFactory.Platform.API.dll"]