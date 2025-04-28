const analyseBtn = document.getElementById('analyseBtn');
const buttonText = document.getElementById('buttonText');
const loaderBar = document.getElementById('loaderBar');

analyseBtn.addEventListener('click', async () => {
    await simulateTextAnimation();
    await simulateLoaderBar();
    
    if (window.isAuthenticated) window.location.href = `${window.BASE_PATH}/Map`;
        else window.location.href = `${window.BASE_PATH}/Connection`;
});

async function simulateTextAnimation() {
    const baseText = "Analyse";
    const delay = ms => new Promise(res => setTimeout(res, ms));
    const cycles = 3;

    for (let c = 0; c < cycles; c++) {
        for (let i = 1; i <= 3; i++) {
            buttonText.textContent = baseText + ".".repeat(i);
            await delay(400);
        }
    }

    await delay(500);

    buttonText.classList.remove('hidden');
    loaderBar.classList.add('hidden');
}

async function simulateLoaderBar() {
    buttonText.classList.add('hidden');
    loaderBar.classList.remove('hidden');

    const segmentCount = 10;
    loaderBar.innerHTML = '';

    for (let i = 0; i < segmentCount; i++) {
        const segment = document.createElement('div');
        segment.classList.add('segment');
        loaderBar.appendChild(segment);
    }

    const segments = loaderBar.querySelectorAll('.segment');
    for (let i = 0; i < segments.length; i++) {
        await new Promise(res => setTimeout(res, 200));
        segments[i].style.opacity = 1;
    }
}
