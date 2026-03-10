FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ServiceB.csproj", "./"]
RUN dotnet restore "ServiceB.csproj"

COPY . .

RUN dotnet publish "ServiceB.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000
COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "ServiceB.dll"]