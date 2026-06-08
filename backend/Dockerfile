FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SentenceBuilder.Api.csproj", "./"]
RUN dotnet restore "SentenceBuilder.Api.csproj"
COPY . ./
WORKDIR "/src"
RUN dotnet publish "SentenceBuilder.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "SentenceBuilder.Api.dll"]
