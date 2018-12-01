FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 61873
EXPOSE 44308

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ShoppingApi/ShoppingApi.csproj", "ShoppingApi/"]
RUN dotnet restore "ShoppingApi/ShoppingApi.csproj"
COPY . .
WORKDIR "/src/ShoppingApi"
RUN dotnet build "ShoppingApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ShoppingApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ShoppingApi.dll"]