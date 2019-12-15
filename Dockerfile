FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY FeatherHttp ./FeatherHttp
COPY Samples ./Samples/
COPY FeatherHttp.sln ./
RUN dotnet restore

RUN dotnet publish Samples/HelloWorld -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
ENV ASPNETCORE_URLS http://+:80
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["./HelloWorld"]