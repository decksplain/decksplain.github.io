# Decksplain - card game rules

A series of classic and novel card game rules provided in a printable format.

![img.png](./.github/images/index.png)

## Development

### Technologies

- Dotnet
  - MVC Razor pages 
- Sass
- GitHub Pages
- PWA with service workers for offline support

The website is built using Dotnet but the end result is statically cached and uploaded to GitHub Pages.

### Requirements

- Dotnet SDK (version defined in the `./global.json`)

### Running

```bash
dotnet run --project ./Decksplain/
```

#### Testing the service worker caching

I decided to go with a [cache only](https://developer.chrome.com/docs/workbox/caching-strategies-overview#cache_only) strategy for offline caching - so it does not fallback to network when the file is not locally available. This allows:

- the user decides they want to store the website for offline use, they click on the installation button which locally caches everything
- this allows the user to be completely offline and the app will still load instantly
- they can also download the website as a PWA for easier offline access
- on page reload, the app checks if there's a different version available and offers the user to update if they want to

To test:

```bash
docker compose up --build decksplain
cd ./static/
npx http-server
```

Using [app.MapStaticAssets();](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/map-static-files?view=aspnetcore-9.0) causes the static files to have their file names modified to have a hash in it to improve caching - however this makes figuring out the file names during runtime difficult. So to figure out what files the service worker needs to cache, the static folder is scanned after it's built to create a JSON file of all the paths that need to be cached.

#### Generating QR codes

QR codes are generated at run time using an API like `/api/qrcodes/aHR0cHM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g_dj0wQkY2bmtkLUgtQQ`, the string at the end is a Base64Url encoded string because it's compatible with both URLs and file systems (whereas base64 produces forward slashes).

You can link to an image like:

```html
<img alt="qr code" src="/api/qrcodes/aHR0cHM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g_dj0wQkY2bmtkLUgtQQ" />
```

To make it easier with generating the encoded string, there's a script:

```bash
> dotnet run ./Scripts/Base64UrlEncode.cs https://www.youtube.com/watch?v=0BF6nkd-H-A
> aHR0cHM6Ly93d3cueW91dHViZS5jb20vd2F0Y2g_dj0wQkY2bmtkLUgtQQ
```

### Releasing

Create a release and versioned tag.
