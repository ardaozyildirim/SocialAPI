FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SocialAPI.API/SocialAPI.API.csproj", "SocialAPI.API/"]
COPY ["SocialAPI.Application/SocialAPI.Application.csproj", "SocialAPI.Application/"]
COPY ["SocialAPI.Domain/SocialAPI.Domain.csproj", "SocialAPI.Domain/"]
COPY ["SocialAPI.Infrastructure/SocialAPI.Infrastructure.csproj", "SocialAPI.Infrastructure/"]
RUN dotnet restore "SocialAPI.API/SocialAPI.API.csproj"
COPY . .
WORKDIR "/src/SocialAPI.API"
RUN dotnet build "SocialAPI.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SocialAPI.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialAPI.API.dll"] 