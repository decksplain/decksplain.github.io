const CACHE_NAME = 'v1';
const pagesToCache = [
    '/',
    '/games/cabo'
];

fetch('asset-manifest.json')

self.addEventListener('install', event =>
{
    event.waitUntil(
        caches.open(CACHE_NAME).then((cache) => {
            return cache.addAll(pagesToCache);
        })
    );
    
    event.waitUntil(
        fetch('asset-manifest.json')
            .then(response => response.json())
            .then(manifest => {
                return caches.open('static-cache-v' + manifest.version).then(cache => {
                    const urlsToCache = manifest.assets.map(a => a.url);
                    return cache.addAll(urlsToCache);
                });
            })
    );
});

self.addEventListener('install', (event) => {
    
});

self.addEventListener('fetch', (event) => {
    event.respondWith(
        caches.match(event.request)
    );
});

self.addEventListener('activate', (event) => {
    event.waitUntil(
        caches.keys().then((keys) =>
            Promise.all(
                keys.map((key) => {
                    if (key !== CACHE_NAME) {
                        return caches.delete(key);
                    }
                })
            )
        )
    );
});
