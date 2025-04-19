
export function renderOverlay(type, data) {
    const overlay = document.getElementById("overlay");
    const content = overlay.querySelector(".overlay-content");

    switch (type) {
        case "missionDetails":
            content.innerHTML = renderMissionDetails(data);
            break;

        case "teamSelection":
            content.innerHTML = renderTeamSelection(data);
            break;
            
        case "missionLaunch":
            content.innerHTML = renderMissionLaunch(data.planetImg);
            break;
            
        default:
            content.innerHTML = "<p>Contenu non disponible</p>";
            break;
    }

    overlay.classList.remove("hidden");
    content.querySelector("#closeOverlay").addEventListener("click", () => {
        overlay.classList.add("hidden");
    });
}

function renderMissionDetails(quest) {
    const enemies = quest.enemies.map(e => `
        <li style="display: flex; flex-direction: column; align-items: center; justify-content:center; gap: 10px; margin-bottom: 6px;">
            <img src="${e.image}" alt="${e.name}" height="24"/>
            <span style="max-width: 100px; word-wrap: break-word; text-align: center;">${e.name}</span>
        </li>
    `).join('');

    const rewards = quest.rewards.map(r => `
        <li style="display: flex; align-items: center; gap: 10px; margin-bottom: 6px;">
            <img src="${r.image}" alt="${r.name}" height="40"/>
            ${r.name}
        </li>
    `).join('');

    return `
        <link rel="stylesheet" href="/css/components/button.css">
        <h4 style="text-align: center;">${quest.title}</h4>
        <p  style="padding: 0 10px;">${quest.description}</p>
        <p  style="padding: 0 10px;"><strong>Difficulté:</strong> ${quest.difficulty}</p>
        <p  style="padding: 0 10px;display: flex;align-items: center;">
            <strong>Récompense:</strong> ${quest.reward}
            <img height="20" src="/img/icons/mustysCoin.png" alt="Coins">
        </p>
        
        <div class="mobs">
            <h5 style="text-align: center;">Mobs à tuer :</h5>
            <ul style="display: flex; justify-content: center; align-items:center; gap:10px; padding: 0 10px; list-style: none; margin: 0;">
                ${enemies}
            </ul>
        </div>
        
        <div class="rewards">
            <h5 style="text-align: center;">Objets à gagner :</h5>
            <ul style="padding: 0 10px; list-style: none; margin: 0;">${rewards}</ul>
        </div>
        <div class="button-container">
            <button id="launchMission">
                <span class="icon">
                    <svg height="303.09363" width="187.41829">
                        <path id="path4" stroke-linecap="round" style="stroke:none;stroke-width:2.81;stroke-linecap:butt;stroke-linejoin:miter;stroke-miterlimit:10;stroke-dasharray:none" d="m 105.04479,7.9451197 c -0.54247,-0.518571 -1.09286,-1.029222 -1.64524,-1.54186 -0.61595,-0.572255 -1.22596,-1.146482 -1.85384,-1.710782 -1.25775,-1.134567 -2.537358,-2.259184 -3.844788,-3.36394 -2.09427,-1.77237697 -5.16413,-1.77038997 -7.26039,0 -1.30742,1.104756 -2.58107,2.227394 -3.83882,3.35798 -0.63782,0.574234 -1.25577,1.156416 -1.87769,1.734625 -0.53847,0.502703 -1.0829,1.003419 -1.61143,1.512084 -0.88818,0.848436 -1.75648,1.70482 -2.61684,2.5651783 -0.14306,0.143062 -0.28811,0.288111 -0.42919,0.43316 -17.060123,17.22703 -28.588524,37.172235 -35.801225,61.735159 -0.125179,0.435146 -0.260293,0.860358 -0.387459,1.293517 -0.04371,0.154984 -0.09339,0.300033 -0.137101,0.455017 -0.0099,0.03377 -0.0079,0.07153 -0.01788,0.105302 -5.766187,20.286964 -8.524101,43.53849 -8.524101,70.45796 0,3.05993 0.05365,6.10596 0.13114,9.14403 l -24.167515,28.14543 c -0.707361,0.82261 -1.160391,1.83199 -1.3074264,2.90495 l -9.80369097,72.24621 c -0.311955,2.29892 0.818631,4.55414 2.84931497,5.67479 2.028696,1.12263 4.538239,0.88221 6.320552,-0.60206 L 51.189952,227.47351 h 24.37615 c -3.33215,5.3072 -5.31316,12.12847 -5.31514,19.66505 0.002,4.51638 0.72127,8.8599 2.15983,12.95305 1.93929,5.40854 7.59023,18.67752 13.573,32.72739 l 2.91687,6.85505 c 0.88221,2.07439 2.91687,3.42156 5.17208,3.41957 1.52003,0.002 2.94072,-0.61199 3.97593,-1.6472 0.49873,-0.49873 0.90804,-1.09879 1.19615,-1.77635 l 3.020198,-7.10143 c 5.93309,-13.93661 11.53635,-27.10227 13.48358,-32.51875 1.42267,-4.06137 2.14394,-8.40687 2.14394,-12.91729 0,-7.53459 -1.983,-14.35387 -5.31515,-19.66107 h 23.65289 l 41.9668,35.01637 c 1.78032,1.48626 4.29185,1.72469 6.32055,0.60205 0.46892,-0.2583 0.88817,-0.5782 1.25378,-0.94381 1.22,-1.22 1.83596,-2.96058 1.59553,-4.73097 l -9.80569,-72.24027 c -0.14704,-1.07297 -0.60007,-2.08235 -1.30743,-2.90495 l -23.45419,-27.31289 c 0.0914,-3.31427 0.14107,-6.63847 0.14306,-9.97857 0,-26.93934 -2.75792,-50.202781 -8.53006,-70.499678 -0.006,-0.02183 -0.006,-0.0457 -0.012,-0.06753 -0.0278,-0.09936 -0.0596,-0.190728 -0.0874,-0.290098 -0.26625,-0.929902 -0.5484,-1.843908 -0.83055,-2.761888 -7.23257,-23.950938 -18.64573,-43.486827 -35.37602,-60.391968 -0.16279,-0.16704 -0.32573,-0.329972 -0.49064,-0.49489 -0.84022,-0.8487053 -1.69701,-1.6927643 -2.57114,-2.5274333 z m 12.504,114.1176503 c 0,6.2669 -2.44,12.16423 -6.87492,16.59915 -4.43293,4.43293 -10.32827,6.87491 -16.599138,6.87491 -12.94511,-0.002 -23.47407,-10.53094 -23.47605,-23.47605 v 0 c 0.004,-12.94709 10.53889,-23.47406 23.47406,-23.47406 6.268888,-0.002 12.166218,2.43801 16.601138,6.87293 4.43491,4.43492 6.8769,10.33025 6.87491,16.60312 z"></path>
                    </svg>
                </span>
                <span class="text">Launch</span>   
                <span class="launch"></span>   
            </button>
        </div>
        <button id="closeOverlay">Fermer</button>
    `;
}

function renderMissionLaunch(planetImg) {
    return `
        <div class="scene">
            <img class="planetLoader" src="${planetImg}" height="90" alt="Planet">
            <div class="spaceship-orbit">
                <img class="spaceship" src="/img/icons/spaceship.png" height="50" alt="Vaisseau">
            </div>
        </div>
        
        <style>
            .overlay-content {background: none;border: none;}
            .scene {
                transform: scale(1.5);
                position: relative;
                width: 200px;
                height: 200px;
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

function renderTeamSelection({ teams, items }) {
    if (!teams || teams.length === 0) {
        return `
            <h4>Vous n’avez encore aucune colonie pour lancer une mission.</h4>
            <button id="closeOverlay">Fermer</button>
        `;
    }

    const teamsHTML = teams.map(team => `
        <option value="${team.id}">${team.name}</option>
    `).join('');

    const itemsHTML = items.map(item => `
        <label>
            ${items.length === 0 || !items ? `<p>Vous n'avez actuellement aucun item</p>` : `
                <input type="checkbox" value="${item.id}"/>
                <img src="${item.image}" height="40" alt="Image"> ${item.name}
            `}
        </label>
    `).join('<br>');

    return `
        <h4>Choisissez une colonie et des objets</h4>

        <label>Colonie :
            <select id="teamSelect">${teamsHTML}</select>
        </label>

        <div style="margin-top: 15px;">
            <h5>Objets à emporter :</h5>
            ${itemsHTML}
        </div>

        <div class="button-container">
            <button id="confirmLaunch">Confirmer</button>
        </div>

        <button id="closeOverlay">Annuler</button>
        
        <style>
            .button-container {
                position: absolute;
                bottom: 5px; right: 5px;     
            }
            #confirmLaunch {
                background-color: #0E0327;
                font-family: 'Judge', sans-serif;
                font-size: 1.5rem;
                color: #9F512D;
                border: 1px solid #9F512D;
                border-radius: 6px;
                padding: 10px 30px;
                cursor: pointer;
            
                position: relative;
                overflow: hidden;
            
                display: flex;
                justify-content: center;
                align-items: center;
            
                min-width: 200px;
                min-height: 50px;
            }
            label {
                display: flex;
                gap: 10px;
                align-items: center;
            }
            select {
                padding: 3px 4px;
                color: rgba(255,255,255,0.53);
                border: 1px solid #284d74;
                border-radius: 4px;
                background-color: #192f44;
            }
            select:focus-visible {
                outline: none;
            }
        </style>
    `;
}