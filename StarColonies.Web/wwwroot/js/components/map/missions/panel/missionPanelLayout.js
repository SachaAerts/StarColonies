import {renderMissionFrame} from '../overlays/renders/missionRenderer.js';

export function createMissionPanelContainer(x, planetId) {
    const panel = document.createElement('div');
    panel.classList.add('quest-panel');
    createMission(panel, planetId);
    panel.style.left = x < 1600 ? '110px' : '-260px';
    panel.style.position = 'relative';
    return panel;
}

export function createMissionScroll(quests) {
    const scroll = document.createElement('div');
    scroll.classList.add('quest-scroll');
    scroll.innerHTML = quests.map(renderMissionFrame).join('');
    return scroll;
}

function createMission(panel, planetId) {
    const link = document.createElement('a');
    link.classList.add('linkCreateMission');
    link.href = `CreateMission/${planetId}`;

    const img = document.createElement('img');
    img.src = '/img/icons/create.png';
    img.height = 30;
    img.alt = 'Create Icon';

    link.appendChild(img);
    panel.appendChild(link);

    const style = document.createElement('style');
    style.textContent = `
        .linkCreateMission {
            position: absolute;
            right: 5px;
            z-index: 100000;
        }
    `;
    document.head.appendChild(style);
}