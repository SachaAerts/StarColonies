import {renderMissionFrame} from '../overlays/renders/missionRenderer.js';

export function createMissionPanelContainer(x) {
    const panel = document.createElement('div');
    panel.classList.add('quest-panel');
    panel.style.left = x < 1600 ? '110px' : '-260px';
    return panel;
}

export function createMissionScroll(quests) {
    const scroll = document.createElement('div');
    scroll.classList.add('quest-scroll');
    scroll.innerHTML = quests.map(renderMissionFrame).join('');
    return scroll;
}
