FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/TorcLib.WebApi/TorcLib.WebApi.csproj", "src/TorcLib.WebApi/"]
COPY ["src/TorcLib.Domain/TorcLib.Domain.csproj", "src/TorcLib.Domain/"]
COPY ["src/TorcLib.Data/TorcLib.Data.csproj", "src/TorcLib.Data/"]
COPY ["src/TorcLib.Application/TorcLib.Application.csproj", "src/TorcLib.Application/"]
COPY ["src/TorcLib.IoC/CashFlow.IoC.csproj", "src/TorcLib.IoC/"]
RUN dotnet restore "src/TorcLib.WebApi/TorcLib.WebApi.csproj"
COPY . .
WORKDIR "/src/src/TorcLib.WebApi"
RUN dotnet build "TorcLib.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TorcLib.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TorcLib.WebApi.dll"]
