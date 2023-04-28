#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
#COPY ["MangoWeb/MangoWeb.csproj", "MangoWeb/"]
COPY MangoWeb.csproj .
#
#RUN dotnet restore "MangoWeb/MangoWeb.csproj"

RUN dotnet restore
COPY . .
WORKDIR "/src/MangoWeb"
RUN dotnet build "MangoWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MangoWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MangoWeb.dll"]

# FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
# WORKDIR /src
# COPY src/*.csproj .
# RUN dotnet restore
# COPY src .
# RUN dotnet publish -c Release -o /publish

# FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
# WORKDIR /publish
# COPY --from=build-env /publish .
# EXPOSE 80
# ENTRYPOINT ["dotnet", "MangoWeb.dll"]