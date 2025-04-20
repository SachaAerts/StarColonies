export function renderLaunchLoading(planetImg) {
    return `
        <div class="scene">
            <img class="planetLoader" src="${planetImg}" height="90" alt="Planet">
            <div class="spaceship-orbit">
                <img class="spaceship" src="/img/icons/spaceship.png" height="50" alt="Vaisseau">
            </div>
        </div>
        ${getStyle()}
    `
}

function getStyle() {
    return `
        <style>
            .overlay-content {background: none; border: none;}
            .scene {
                transform: scale(1.5);
                position: relative;
                height: 200px; width: 200px;
                margin: 100px auto;
            }
        
            .planetLoader {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                z-index: 1;
            }
        
            .spaceship-orbit {
                position: absolute;
                width: 100%;
                height: 100%;
                animation: rotateOrbit 5s linear infinite;
            }
        
            .spaceship {
                position: absolute;
                top: 0;
                left: 50%;
                transform: translateX(-50%) rotate(90deg);
                transform-origin: center center;
                animation: counterRotate 5s linear infinite;
            }
        
            @keyframes rotateOrbit {
                from {
                    transform: rotate(0deg);
                }
                to {
                    transform: rotate(360deg);
                }
            }
        </style>
    `
}