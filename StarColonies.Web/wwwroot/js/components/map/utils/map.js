const map = document.getElementById("map");
const planetContainer = map.getElementsByClassName("planet-container");
const viewport = document.getElementById("viewport");

const mapSize = 3000;
const minDistance = 50;
const maxAttempts = 30;

map.style.width = `${mapSize}px`;
map.style.height = `${mapSize}px`;

let posX = -mapSize / 4;
let posY = -mapSize / 4;

updateMapPosition();

let isDragging = false;
let lastMouseX, lastMouseY;

const stars        = ["img/stars/large.png", "img/stars/medium.png"];
const stars_small  = ["img/stars/small.png", "img/stars/little-less-small.png"];
const planetImages = ["img/stars/planet.png"];
const meteorImages = ["img/stars/meteor.png"];

const placedObjects = [];

function updateMapPosition() {
    const maxX = window.innerWidth - mapSize + 100;
    const maxY = window.innerHeight - mapSize + 100;
    const minX = -100;
    const minY = -100;

    posX = Math.max(maxX, Math.min(minX, posX));
    posY = Math.max(maxY, Math.min(minY, posY));

    map.style.transform = `translate(${posX}px, ${posY}px)`;
}

map.addEventListener("mousedown", (e) => {
    isDragging = true;
    lastMouseX = e.clientX;
    lastMouseY = e.clientY;
});

document.addEventListener("mousemove", (e) => {
    if (isDragging) {
        const dx = e.clientX - lastMouseX;
        const dy = e.clientY - lastMouseY;
        posX += dx;
        posY += dy;
        lastMouseX = e.clientX;
        lastMouseY = e.clientY;
        updateMapPosition();
    }
});

document.addEventListener("mouseup", () => {
    isDragging = false;
});


function isOverlapping(x, y, size) {
    for (let obj of placedObjects) {
        const dx = obj.x - x;
        const dy = obj.y - y;
        const distance = Math.sqrt(dx * dx + dy * dy);
        if (distance < size + minDistance) return true;
    }
    return false;
}

function placeElement(className, imageArray, size, density) {
    let count = Math.floor(mapSize * mapSize * density / 100000);

    for (let i = 0; i < count; i++) {
        let x, y, attempts = 0;
        do {
            x = Math.random() * mapSize;
            y = Math.random() * mapSize;
            attempts++;
        } while (isOverlapping(x, y, size) && attempts < maxAttempts);

        if (attempts < maxAttempts) {
            const element = document.createElement("div");
            const img = imageArray[Math.floor(Math.random() * imageArray.length)];
            element.classList.add(className);
            element.style.position = "absolute";
            element.style.width = `${size}px`;
            element.style.height = `${size}px`;
            element.style.background = `url(${img}) center/cover`;
            element.style.top = `${y}px`;
            element.style.left = `${x}px`;

            map.appendChild(element);
            placedObjects.push({ x, y, size });
        }
    }
}

placeElement("star",       stars,        20, 1.5);
placeElement("small-star", stars_small,  10, 2.5);
placeElement("planet",     planetImages, 40, 0.05);
placeElement("meteor",     meteorImages, 35, 0.1); 