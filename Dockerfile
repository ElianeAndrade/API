FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebCredentialsApi/WebCredentialsApi.csproj", "WebCredentialsApi/"]
RUN dotnet restore "WebCredentialsApi/WebCredentialsApi.csproj"
COPY . .
WORKDIR "/src/WebCredentialsApi"
RUN dotnet build "WebCredentialsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebCredentialsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebCredentialsApi.dll"]
