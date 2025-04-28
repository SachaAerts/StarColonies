import { renderOverlay } from './overlayRenderer.js';
import { waitForElement } from '../utils/domUtils.js';
import { getSelectedMission } from './missionContext.js';

export function handleLaunchClick(data) {
    renderOverlay("teamSelection", {
        teams: data.teams ?? [],
        items: data.items ?? []
    });

    waitForElement("#confirmLaunch", confirmBtn => {
        confirmBtn.addEventListener("click", async () => {
            const selectedTeam = document.getElementById("teamSelect")?.value;
            const selectedItems = Array.from(document.querySelectorAll('input[type="checkbox"]:checked'))
                .map(i => parseInt(i.value));

            renderOverlay("missionLaunch", { planetImg: data.image });
            await sendMissionRequest(selectedTeam, selectedItems);
        });
    });
}

async function sendMissionRequest(selectedTeam, selectedItems) {
    const mission = getSelectedMission();
    
    if (!mission) {
        console.error("Aucune mission sélectionnée");
        return;
    }

    const body = {
        MissionId: parseInt(mission.id),
        ColonyId: parseInt(selectedTeam),
        ItemIds: selectedItems,
    };

    try {
        const response = await post(body);
        
        const result = await response.json();
        
        setTimeout(() => renderOverlay("missionResult", result.result), 2000);

    } catch (error) {
        console.error("Erreur lors de l’envoi de la mission :", error);
    }
}

async function post(body) {
    const response = await fetch("/Map?handler=ResolveMission", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "RequestVerificationToken":
                document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: JSON.stringify(body)
    });

    if (!response.ok) {
        const error = await response.text();
        renderOverlay("missionResult", {
            description: "Erreur réseau : " + error,
            isSuccess: false,
            rewards: []
        });
        return;
    }
    return response;
}
