export function createPlanetContainer(x, y, image, onClick) {
    const div = document.createElement('div');
    div.classList.add('planet-container');
    div.style.top = `${y}px`;
    div.style.left = `${x}px`;
    div.style.backgroundImage = `url("${image}")`;
    div.addEventListener('click', onClick);
    return div;
}
