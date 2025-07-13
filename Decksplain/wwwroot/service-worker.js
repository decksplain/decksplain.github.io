const CACHE_NAME = 'static-cache-v';

self.addEventListener('install', event =>
{
    event.waitUntil(
        fetch('asset-manifest.json')
            .then(response => response.json())
            .then(manifest => {
                return caches.open(CACHE_NAME).then(cache => {
                    return cache.addAll(manifest.assets);
                });
            })
    );
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
