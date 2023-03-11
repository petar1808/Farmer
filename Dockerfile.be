FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .

RUN dotnet restore "WebApi/WebApi.csproj"
RUN dotnet publish "WebApi/WebApi.csproj" -o /app/published-app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime

EXPOSE 5000
EXPOSE 5001

WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/WebApi.dll" ]