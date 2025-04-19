import { truncateText } from './utils.js';
import { renderOverlay } from './MissionLaunchRenderer.js';

export function createQuestPanel(planet) {
    const panel = document.createElement('div');
    panel.classList.add('quest-panel');
    panel.style.left = planet.x < 1600 ? '110px' : '-260px';

    const scroll = document.createElement('div');
    scroll.classList.add('quest-scroll');
    scroll.innerHTML = planet.quests.map((quest, i) => `
        <div class="quest-frame" data-index="${i}">
            <h3>${quest.title}</h3>
            <p style="font-family: 'Pixelify Sans', sans-serif;font-size: 0.8rem">
                ${truncateText(quest.description, 100)}
            </p>
            <div style="display: flex; justify-content: space-between; align-items: center;">
                <p>Difficulty : ${quest.difficulty}</p>
                <small>Rewards : ${quest.reward} <img height="20" src="/img/icons/mustysCoin.png" alt="Coins"></small>
            </div>
        </div>
    `).join('');

    panel.appendChild(scroll);

    scroll.querySelectorAll('.quest-frame').forEach((el, index) => {
        el.addEventListener('click', (event) => {
            event.stopPropagation();

            const quest = planet.quests[index];
            renderOverlay("missionDetails", quest);

            setTimeout(() => {
                const launchBtn = document.querySelector("#launchMission");
                if (launchBtn) {
                    launchBtn.addEventListener("click", () => {
                        renderOverlay("teamSelection", {
                            teams: planet.teams,
                            items: [
                                { id: 1, name: "Kit de soins", image: "/img/items/medkit.png" },
                                { id: 2, name: "Amplificateur", image: "/img/items/booster.png" }
                            ]
                        });

                        setTimeout(() => {
                            const confirmBtn = document.querySelector("#confirmLaunch");
                            if (confirmBtn) {
                                confirmBtn.addEventListener("click", () => {
                                    const selectedTeam = document.getElementById("teamSelect")?.value;
                                    const selectedItems = Array.from(document.querySelectorAll('input[type="checkbox"]:checked')).map(i => parseInt(i.value));

                                    renderOverlay("missionLaunch", {
                                        planetImg: planet.image,
                                        teamId: selectedTeam,
                                        items: selectedItems
                                    });
                                });
                            }
                        }, 50);
                    });
                }
            }, 50);
        });
    });

    return panel;
}
