FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src
COPY ["CriptographyAPIs/CriptographyAPIs.csproj", "CriptographyAPIs/"]
RUN dotnet restore "CriptographyAPIs/CriptographyAPIs.csproj"
COPY . .
WORKDIR "/src/CriptographyAPIs"
RUN dotnet build "CriptographyAPIs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CriptographyAPIs.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CriptographyAPIs.dll", "--environment=Development"]
