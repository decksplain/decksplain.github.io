#!/bin/bash
set -e

echo "Starting the site..."
nohup dotnet run --no-build --property:Configuration=Release --project ./Decksplain/Decksplain.csproj --urls "http://localhost:5000" > output.log 2>&1 &
echo $! > website.pid

# Wait for server
for i in {1..30}; do
  if curl -s http://localhost:5000 > /dev/null; then
    echo "Website is up!"
    break
  fi
  sleep 2
done

echo "Mirroring site..."
mkdir -p static
rm -rf static/*
wget \
  --recursive \
  --no-host-directories \
  --directory-prefix=static \
  --timeout=30 \
  --no-parent \
  --adjust-extension \
  http://localhost:5000

cp ./Decksplain/wwwroot/service-worker.js ./static/service-worker.js
cp ./Decksplain/wwwroot/images/logo-square.svg ./static/images/logo-square.svg

echo "Restructure HTML files into folders with index.html (skip root index.html)..."
cd static
find . -type f -name '*.html' ! -name 'index.html' | while read -r file; do
  if [[ "$file" == "./index.html" ]]; then continue; fi
  dir=$(dirname "$file")
  base=$(basename "$file" .html)
  mkdir -p "$dir/$base"
  mv "$file" "$dir/$base/index.html"
done

echo "Add base path to all links..."
BASE_PATH="${BASE_PATH%/}/"  # Ensure trailing slash
ESC_BASE_PATH=$(echo "$BASE_PATH" | sed 's/\//\\\//g')

# Fix href/src links
find . -type f -name '*.html' -exec sed -i -E \
"s|href=\"/([^\"]*)\"|href=\"$ESC_BASE_PATH\\1\"|g" {} +

find . -type f -name '*.html' -exec sed -i -E \
"s|src=\"/([^\"]*)\"|src=\"$ESC_BASE_PATH\\1\"|g" {} +

cd ..

echo "Stopping server..."
kill $(cat website.pid)

echo "Static export complete! Files in ./static"
