const CACHE_NAME = 'static-cache';

async function updateCache() {
    const manifestResponse = await fetch('/asset-manifest.json');
    const manifest = await manifestResponse.json();

    await caches.delete(CACHE_NAME);

    const cache = await caches.open(CACHE_NAME);

    await cache.addAll(manifest.assets);

    await cache.put('/version.json', new Response(JSON.stringify({ version: manifest.version }), {
        headers: { 'Content-Type': 'application/json' }
    }));
}

self.addEventListener('install', event => {
    event.waitUntil(
        (async () => {
            await updateCache();
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

self.addEventListener('message', async event => {
    if (event.data === "UPDATE_CACHE") {
        await updateCache();

        event.source.postMessage("CACHE_UPDATED");
    }
});
