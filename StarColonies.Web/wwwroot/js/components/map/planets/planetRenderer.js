export function createPlanetContainer(x, y, image, onClick) {
    const div = document.createElement('div');
    const BASE_PATH = window.BASE_PATH || "";
    div.classList.add('planet-container');
    div.style.top = `${y}px`;
    div.style.left = `${x}px`;
    div.style.backgroundImage = `url("${BASE_PATH}/img/planets/${image}")`;
    div.addEventListener('click', onClick);
    return div;
}
