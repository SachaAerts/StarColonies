import {renderMissionFrame} from '../overlays/renders/missionRenderer.js';

export function createMissionPanelContainer(x, planetId) {
    const panel = document.createElement('div');
    panel.classList.add('quest-panel');
    if (window.isAdmin) {
        createMission(panel, planetId);
    }
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
    
    const BASE_PATH = window.BASE_PATH || "";
    const img = document.createElement('img');
    img.src = BASE_PATH + '/img/icons/modify.png';
    img.height = 20;
    img.style.cursor = 'pointer';
    img.style.position = 'absolute';
    img.style.top = '5px';
    img.style.right = '5px';
    img.style.right = '5px';
    img.style.zIndex = '1000000000';
    img.alt = 'Create Icon';

    link.appendChild(img);
    panel.appendChild(link);
}