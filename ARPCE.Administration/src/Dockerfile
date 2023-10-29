#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebUI/WebUI.csproj", "WebWebUI/"]
COPY [".", "."]
#COPY . /src/
RUN dotnet restore "WebUI/WebUI.csproj"
#COPY . /src/

#COPY FNPOSInscription.WebApi/Resources/* /src/Resources
# /src/FNPOSInscription.WebApi/Resources

WORKDIR "/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
EXPOSE 5000
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
#COPY FNPOSInscription.WebApi/Resources/* /app/Resources/
ENTRYPOINT ["dotnet", "WebUI.dll"]
