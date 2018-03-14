FROM microsoft/dotnet:2.0-sdk AS builder

WORKDIR /app
COPY src /app
RUN dotnet restore Service.csproj
RUN dotnet publish --configuration Release --output ./out


FROM microsoft/aspnetcore:2.0
LABEL maintainer "frank@pommerening-online.de"
ENV REFRESHED_AT 2018-03-14

ENV ASPNETCORE_URLS http://0.0.0.0:7000
EXPOSE 7000

WORKDIR /app/
COPY --from=builder /app/out/* ./

ENTRYPOINT ["dotnet", "Service.dll"]