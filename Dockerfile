# Brug Microsofts officielle .NET Core runtime image som base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Byg appen
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Book-REST-Service/Book-REST-Service.csproj", "Book-REST-Service/"]
RUN dotnet restore "Book-REST-Service/Book-REST-Service.csproj"
COPY . .
WORKDIR "/src/Book-REST-Service"
RUN dotnet build "Book-REST-Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Book-REST-Service.csproj" -c Release -o /app/publish

# Byg det endelige image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Book-REST-Service.dll"]
