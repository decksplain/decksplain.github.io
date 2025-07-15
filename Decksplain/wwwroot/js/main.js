async function installServiceWorker() {
    if (!('serviceWorker' in navigator)) {
        return;
    }

    try {
        const registration = await navigator.serviceWorker.register('/service-worker.js')

        console.log('Service worker registered:', registration);
    } catch (err) {
        console.error('Service worker registration failed:', err);
    }
}

async function updateServiceWorkerCache() {
    return new Promise(resolve => {
        navigator.serviceWorker.controller.postMessage("UPDATE_CACHE");

        navigator.serviceWorker.addEventListener('message', event => {
            if (event.data === 'CACHE_UPDATED') {
                resolve();
            }
        });
    })
}

async function uninstallServiceWorker() {
    const registrations = await navigator.serviceWorker.getRegistrations();

    for (const registration of registrations) {
        await registration.unregister();
    }
}

async function getManifest() {
    try
    {
        const manifestResponse = await fetch('/asset-manifest.json');
        return await manifestResponse.json();
    }
    catch
    {
        // Probably developing locally.
        return null;
    }
}

async function getCurrentVersion() {
    try
    {
        // Only available if service worker is installed.
        const response = await fetch('/version.json');
        const json = await response.json();

        return json.version;
    }
    catch
    {
        return null;
    }
}

async function setupInstallButton() {
    const manifest = await getManifest();

    if (!manifest) {
        return;
    }

    const currentVersion = await getCurrentVersion();

    const button = document.querySelector('#enable-offline')

    if (currentVersion) {
        if (currentVersion !== manifest.version) {
            button.innerHTML = `Update cache from ${currentVersion} to ${manifest.version}`;
            button.style.display = "";
        }
    } else {
        button.style.display = "";
    }

    button
        .addEventListener('click', async () => {
            if (currentVersion) {
                await updateServiceWorkerCache();

                location.reload();
            } else {
                await installServiceWorker();

                alert("Website is now available offline. Considering installing the app too!");

                navigator.serviceWorker.ready.then(() => {
                    console.log('Service worker ready and controlling the page');
                    location.reload();
                });
            }
        });
}

async function setupUninstallButton() {
    if (!navigator.serviceWorker.controller)
        return;

    const button = document.querySelector('#disable-offline');

    button.style.display = "";

    button.addEventListener('click', async () => {
        await uninstallServiceWorker();

        location.reload();
    })
}

setupInstallButton();
setupUninstallButton();
