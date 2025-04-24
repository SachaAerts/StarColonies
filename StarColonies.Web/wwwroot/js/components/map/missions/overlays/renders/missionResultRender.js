
import { notifyMissionResult } from '../../../../notifications/notifyMissionResult.js';

export function renderMissionResult(data) {

    console.log("Mission result data: ", data);
    
    const result = data.result, 
          mission = data.mission;
    
    const livingColony = result.livingColony;
    const overcomingMission = result.overcomingMission;
    const isSuccess = livingColony && overcomingMission;
    
    notifyMissionResult(result, mission);
    
    const render = `
        <style>
            .notyf {
                z-index: 99999;
            }
        </style>
        <h4>Résultat de la mission</h4>
        <p>${isSuccess ? "Mission réussie !" : "Échec de la mission"}</p>

        <p style="color: ${isSuccess ? 'lightgreen' : 'crimson'};">
            ${result.resultMessage}
        </p>

        ${isSuccess ? `
            <h5>Récompenses :</h5>
            <p><strong>Musty gagnés :</strong> ${mission.coinsReward}</p>
        ` : `
            <h5>Pas de récompense : l’équipe ne gagne rien.</h5>
        `}
        
        <button id="closeOverlay">Fermer</button>
    `;
    
    refreshEvent("closeOverlay");
    
    return render;
}

function refreshEvent(id) {
    setTimeout(() => {
        const closeButton = document.getElementById(id);
        if (closeButton) {
            closeButton.addEventListener("click", () => {
                location.reload();
            });
        }
    }, 0);
}
