import { truncateText } from './utils.js';
import { renderOverlay } from './MissionLaunchRenderer.js';

export function createQuestPanel(data) {
    const panel = createQuestPanelContainer(data.x);
    const scroll = createQuestScroll(data);

    panel.appendChild(scroll);
    attachQuestListeners(scroll, data);

    return panel;
}

function createQuestPanelContainer(x) {
    const panel = document.createElement('div');
    panel.classList.add('quest-panel');
    panel.style.left = x < 1600 ? '110px' : '-260px';
    return panel;
}

function createQuestScroll(data) {
    const scroll = document.createElement('div');
    scroll.classList.add('quest-scroll');
    scroll.innerHTML = data.quests.map((quest, i) => renderQuestFrame(quest, i)).join('');
    return scroll;
}

function renderQuestFrame(quest, index) {
    return `
        <div class="quest-frame" data-index="${index}">
            <h3>${quest.title}</h3>
            <p style="font-family: 'Pixelify Sans', sans-serif; font-size: 0.8rem;">
                ${truncateText(quest.description, 100)}
            </p>
            <div style="display: flex; justify-content: space-between; align-items: center;">
                <p>Difficulty : ${quest.difficulty}</p>
                <small>Rewards : ${quest.reward} <img height="20" src="/img/icons/mustysCoin.png" alt="Coins"></small>
            </div>
        </div>
    `;
}

function attachQuestListeners(scroll, data) {
    const questFrames = scroll.querySelectorAll('.quest-frame');

    questFrames.forEach(el => {
        el.addEventListener('click', event => {
            event.stopPropagation();

            const index = parseInt(el.getAttribute('data-index'));
            const quest = data.quests[index];

            renderOverlay("missionDetails", quest);
            waitForElement("#launchMission", launchBtn =>
                launchBtn.addEventListener("click", () => { handleLaunchClick(data); })
            );
        });
    });
}

function handleLaunchClick(data) {
    renderOverlay("teamSelection", {
        teams: data.teams ?? [],
        items: data.items ?? []
    });

    waitForElement("#confirmLaunch", confirmBtn => {
        confirmBtn.addEventListener("click", () => {
            const selectedTeam = document.getElementById("teamSelect")?.value;
            const selectedItems = Array.from(document.querySelectorAll('input[type="checkbox"]:checked')).map(i => parseInt(i.value));

            renderOverlay("missionLaunch", {
                planetImg: data.image,
                teamId: selectedTeam,
                items: selectedItems
            });
        });
    });
}

function waitForElement(selector, callback, retry = 5, delay = 50) {
    const element = document.querySelector(selector);
    if (element) {
        callback(element);
    } else if (retry > 0) {
        setTimeout(() => waitForElement(selector, callback, retry - 1, delay), delay);
    }
}