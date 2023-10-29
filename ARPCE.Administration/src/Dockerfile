#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FNPOSInscription.WebApi/FNPOSInscription.WebApi.csproj", "FNPOSInscription.WebApi/"]
COPY [".", "."]
#COPY . /src/
RUN dotnet restore "FNPOSInscription.WebApi/FNPOSInscription.WebApi.csproj"
#COPY . /src/

#COPY FNPOSInscription.WebApi/Resources/* /src/Resources
# /src/FNPOSInscription.WebApi/Resources

WORKDIR "/src/FNPOSInscription.WebApi"
RUN dotnet build "FNPOSInscription.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FNPOSInscription.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
EXPOSE 587
EXPOSE 25
WORKDIR /app
COPY --from=publish /app/publish .
COPY FNPOSInscription.WebApi/Resources/* /app/Resources/
ENTRYPOINT ["dotnet", "FNPOSInscription.WebApi.dll"]
