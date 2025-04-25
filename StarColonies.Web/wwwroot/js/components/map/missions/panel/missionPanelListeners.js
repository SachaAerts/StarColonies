import { handleLaunchClick } from '../missionLaunch.js';
import { setSelectedMission } from '../missionContext.js';
import { waitForElement } from '../../utils/domUtils.js';
import { renderOverlay } from '../overlayRenderer.js';

export function attachQuestListeners(scroll, data) {
    const questFrames = scroll.querySelectorAll('.quest-frame');

    questFrames.forEach(el => {
        el.addEventListener('click', event => {
            if (event.target.classList.contains('delete-mission') 
                || event.target.closest('.delete-mission')) {
                return;
            }

            event.stopPropagation();

            const index = parseInt(el.dataset.index);
            const quest = data.quests[index];

            setSelectedMission(quest);
            scroll.querySelectorAll(".quest-frame").forEach(q => q.removeAttribute("selected"));
            el.setAttribute("selected", "true");

            renderOverlay("missionDetails", quest);

            waitForElement("#launchMission", launchBtn => {
                launchBtn.addEventListener("click", () => handleLaunchClick(data));
            });
        });
    });
}
