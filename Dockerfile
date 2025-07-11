FROM mcr.microsoft.com/dotnet/sdk:9.0

WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet build --no-restore --configuration Release

EXPOSE 5000

CMD ["bash", "/app/run-and-mirror.sh"]
