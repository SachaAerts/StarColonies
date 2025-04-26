import { createMissionPanelContainer, createMissionScroll } from './panel/missionPanelLayout.js';
import { attachQuestListeners } from './panel/missionPanelListeners.js';

export function createQuestPanel(data) {
    const panel = createMissionPanelContainer(data.x, data.id);
    const scroll = createMissionScroll(data.quests);
    panel.appendChild(scroll);
    attachQuestListeners(scroll, data);
    return panel;
}