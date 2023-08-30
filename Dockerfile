#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

RUN apt-get update \ 
    && apt-get install -y --no-install-recommends libgdiplus libc6-dev \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*
RUN cd /usr/lib && ln -s libgdiplus.so gdiplus.dll

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["wise_api.csproj", "."]
RUN dotnet restore "wise_api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "wise_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wise_api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wise_api.dll"]