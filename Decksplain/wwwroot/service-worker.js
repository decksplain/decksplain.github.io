const CACHE_NAME = 'static-cache-v';

self.addEventListener('install', event => {
    event.waitUntil(
        (async () => {
            const manifestResponse = await fetch('/asset-manifest.json');
            const manifest = await manifestResponse.json();

            const cache = await caches.open(CACHE_NAME);

            await cache.addAll(manifest.assets);

            await cache.put('/version.json', new Response(JSON.stringify({ version: manifest.version }), {
                headers: { 'Content-Type': 'application/json' }
            }));
        })()
    );
});

self.addEventListener('fetch', (event) => {
    const url = new URL(event.request.url);
    
    if (url.pathname === '/asset-manifest.json') {
        return fetch(url);
    }
    
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
