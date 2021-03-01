#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Seventh.DGuard.API/Seventh.DGuard.API.csproj", "Seventh.DGuard.API/"]
COPY ["Seventh.DGuard.Application/Seventh.DGuard.Application.csproj", "Seventh.DGuard.Application/"]
COPY ["Seventh.DGuard.Domain/Seventh.DGuard.Domain.csproj", "Seventh.DGuard.Domain/"]
COPY ["Seventh.DGuard.Infra.Data.File/Seventh.DGuard.Infra.Data.File.csproj", "Seventh.DGuard.Infra.Data.File/"]
COPY ["Seventh.DGuard.Infra.Data.Sql/Seventh.DGuard.Infra.Data.Sql.csproj", "Seventh.DGuard.Infra.Data.Sql/"]
RUN dotnet restore "Seventh.DGuard.API/Seventh.DGuard.API.csproj"
COPY . .
WORKDIR "/src/Seventh.DGuard.API"
RUN dotnet build "Seventh.DGuard.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Seventh.DGuard.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seventh.DGuard.API.dll"]