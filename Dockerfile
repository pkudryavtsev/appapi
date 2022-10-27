FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /sln

COPY ./src ./src
RUN dotnet publish ./src/App.Api.csproj -c Release -o ./publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /sln/publish .
ENTRYPOINT [ "dotnet", "App.Api.dll" ]
