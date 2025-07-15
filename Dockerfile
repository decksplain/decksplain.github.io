FROM mcr.microsoft.com/dotnet/sdk:10.0.100-preview.4

WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet build --no-restore --configuration Release

EXPOSE 5000

CMD ["bash", "/app/Scripts/run-and-mirror.sh"]
