function installServiceWorker() {
    if ('serviceWorker' in navigator) {
        navigator.serviceWorker.register('/service-worker.js')
            .then(reg => console.log('Service worker registered:', reg))
            .catch(err => console.error('Service worker registration failed:', err));
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
            button.innerHTML = `Upgrade from ${currentVersion} to ${manifest.version}`;
            button.style.display = "";
        }
    } else {
        button.innerHTML = `Install ${manifest.version}`;
        button.style.display = "";
    }

    button
        .addEventListener('click', () => {
           installServiceWorker(); 
        });
}

async function setupUninstallButton() {
    if (!navigator.serviceWorker.controller)
        return;

    const button = document.querySelector('#disable-offline');
    
    button.style.display = "";
    
    button.addEventListener('click', () => {
        navigator.serviceWorker.getRegistrations().then(registrations => {
            for (const registration of registrations) {
                registration.unregister();
            }
        });
    })
}

setupInstallButton();
setupUninstallButton();
