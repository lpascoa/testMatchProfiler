#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src

#COPY ["../TestMatchProfile.Infrastructure.Shared/.", "TestMatchProfile.Infrastructure.Shared/"]
#COPY ["src/TestMatchProfile.Infrastructure.Persistence/TestMatchProfile.Infrastructure.Persistence.csproj", "TestMatchProfile.Infrastructure.Persistence/"]
#COPY ["src/TestMatchProfile.Domain/TestMatchProfile.Domain.csproj", "TestMatchProfile.Domain/"]
#COPY ["src/TestMatchProfile.Application/TestMatchProfile.Application.csproj", "TestMatchProfile.Application/"]
#COPY ["src/TestMatchProfile.WebApi/TestMatchProfile.WebApi.csproj", "TestMatchProfile.WebApi/"]

#RUN dotnet restore "src/TestMatchProfile.WebApi/TestMatchProfile.WebApi.csproj"
COPY ./../../. .
#WORKDIR "/src/src/TestMatchProfile.WebApi"
#RUN dotnet build "TestMatchProfile.WebApi.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "TestMatchProfile.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestMatchProfile.WebApi.dll"]
