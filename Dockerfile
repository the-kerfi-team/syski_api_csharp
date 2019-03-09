FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80/tcp
EXPOSE 443/tcp

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["csharp/csharp.csproj", "csharp/"]
RUN dotnet restore "csharp/csharp.csproj"
COPY . .
WORKDIR "/src/csharp"
RUN dotnet build "csharp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "csharp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "csharp.dll"]
