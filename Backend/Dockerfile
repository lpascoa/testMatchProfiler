FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS build

WORKDIR /src
COPY ["src/TestMatchProfile.WebApi/TestMatchProfile.WebApi.csproj", "src/TestMatchProfile.WebApi/"]
COPY ["src/TestMatchProfile.Infrastructure.Shared/TestMatchProfile.Infrastructure.Shared.csproj", "src/TestMatchProfile.Infrastructure.Shared/"]
COPY ["src/TestMatchProfile.Infrastructure.Persistence/TestMatchProfile.Infrastructure.Persistence.csproj", "src/TestMatchProfile.Infrastructure.Persistence/"]
COPY ["src/TestMatchProfile.Domain/TestMatchProfile.Domain.csproj", "src/TestMatchProfile.Domain/"]
COPY ["src/TestMatchProfile.Application/TestMatchProfile.Application.csproj", "src/TestMatchProfile.Application/"]
RUN dotnet restore "src/TestMatchProfile.WebApi/TestMatchProfile.WebApi.csproj"
COPY . .
WORKDIR "/src/src/TestMatchProfile.WebApi"
RUN dotnet build "TestMatchProfile.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestMatchProfile.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestMatchProfile.WebApi.dll"]