FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY TTMS.Internship.Services.TelemetryRead.csproj .
RUN dotnet restore "TTMS.Internship.Services.TelemetryRead.csproj"

COPY . .
RUN dotnet build "TTMS.Internship.Services.TelemetryRead.csproj" -c Release -o /build

FROM build AS publish
RUN dotnet publish "TTMS.Internship.Services.TelemetryRead.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /app
COPY --from=publish /publish .
ENTRYPOINT ["dotnet", "TTMS.Internship.Services.TelemetryRead.dll"]