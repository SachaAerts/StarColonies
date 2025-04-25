import { truncateText } from '../../../utils/utils.js';
import { NotificationContext } from '../../../../notifications/NotificationContext.js'
import { NotyfStrategy } from '../../../../notifications/NotyStrategy.js'

const notify = new NotificationContext(new NotyfStrategy());
let deleteListenerInstalled = false;

export function renderMissionFrame(quest, index) {
    const html = `
        <div class="quest-frame" data-index="${index}">
            <h3>${quest.title}</h3> 
            <img src="/img/icons/supprimer.png" 
                 alt="Supprimer" 
                 height="20" 
                 class="delete-mission"
                 data-id="${quest.id}"
                 style="position: absolute; top: 5px; right: 5px; cursor: pointer; z-index: 1000000000;">
            <p style="font-family: 'Pixelify Sans', sans-serif; font-size: 0.8rem;">
                ${truncateText(quest.description, 100)}
            </p>
            <div style="display: flex; justify-content: space-between; align-items: center;">
                <p>Difficulty : ${quest.difficulty}</p>
                <small>Rewards : ${quest.reward} <img height="20" src="/img/icons/mustysCoin.png" alt="Coins"></small>
            </div>
        </div>
        <style>
            .delete-mission {
                position: absolute; 
                top: 5px; right: 5px; 
                cursor: pointer; 
                z-index: 1000000000;
            }
            .delete-mission:hover {
                transform: scale(0.8);
            }
        </style>
    `;

    if (!deleteListenerInstalled) {
        setupDeleteListener();
        deleteListenerInstalled = true;
    }

    return html;
}

function setupDeleteListener() {
    document.addEventListener("click", async (event) => {
        if (event.target.classList.contains("delete-mission")) {
            event.preventDefault();
            event.stopPropagation();

            const missionId = event.target.dataset.id;
            const questFrame = event.target.closest(".quest-frame");

            if (confirm("Would you like to delete this mission?")) {
                try {
                    const response = await fetch(`/DeleteMission?id=${missionId}`, {
                        method: "POST",
                        headers: {
                            "RequestVerificationToken": document.querySelector('input[name="__RequestVerificationToken"]')?.value ?? ''
                        }
                    });

                    const result = await response.json();

                    if (result.success) {
                        if (questFrame) {
                            questFrame.remove();
                        }
                    } else {
                        notify.error("Erreur lors de la suppression !");
                    }
                } catch (error) {
                    notify.error("Erreur de communication avec le serveur.");
                }
            }
        }
    });
}