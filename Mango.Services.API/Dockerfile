#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["Mango.Services.API/Mango.Services.ProductAPI.csproj", "Mango.Services.API/"]
#RUN dotnet restore "Mango.Services.API/Mango.Services.ProductAPI.csproj"
#COPY . .
#WORKDIR "/src/Mango.Services.API"
#RUN dotnet build "Mango.Services.ProductAPI.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Mango.Services.ProductAPI.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Mango.Services.ProductAPI.dll"]

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env
 WORKDIR /Mango.Services.API
 #COPY MangoWeb/*.csproj .
  COPY . .

 RUN dotnet restore
 RUN dotnet publish -c Release -o /publish

 FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
 WORKDIR /publish
 COPY --from=build-env /publish .
 EXPOSE 80
 ENTRYPOINT ["dotnet", "Mango.Services.ProductAPI.dll"]