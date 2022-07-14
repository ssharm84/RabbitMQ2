FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Payment/Payment.csproj", "Payment/"]
RUN dotnet restore "Payment\Payment.csproj"
COPY . .
WORKDIR "/src/Payment"
RUN dotnet build "Payment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.dll"]
