import {renderOverlay} from "../../overlayRenderer.js";

export function renderTeamSelection(data) {
    const notyf = new Notyf({
        duration: 8000,
        ripple: true,
        position: { x: 'right', y: 'top' }
    });
    
    if (!data.teams || data.teams.length === 0) {
        notyf.error("Aucune colonie disponible pour cette mission.");
        return renderOverlay("missionDetails", data);
    }

    const teamsHTML = data.teams.map(team => `
        <option value="${team.id}">${team.name}</option>
    `).join('');

    const itemsHTML = data.items.map(item => `
        <label>
            ${data.items.length === 0 || !data.items ? `<p>Vous n'avez actuellement aucun item</p>` : `
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
        ${getStyle()}
    `;
}

function getStyle() {
    return `
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
    `
}