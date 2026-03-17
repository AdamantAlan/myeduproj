FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ServiceA.csproj", "./"]
RUN dotnet restore "ServiceA.csproj"

COPY . .

RUN dotnet publish "ServiceA.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:5000
COPY --from=build /app/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "ServiceA.dll"]