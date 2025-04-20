import { waitForElement } from "../utils/domUtils.js";
import { MissionDetailsCommand } from "./overlays/missionDetailsCommand.js";
import { TeamSelectionCommand } from "./overlays/teamSelectionCommand.js";
import { MissionLaunchCommand } from "./overlays/missionLaunchCommand.js";
import { MissionResultCommand } from "./overlays/missionResultCommand.js";
import { DefaultCommand } from "./overlays/defaultCommand.js";

const overlayCommands = {
    missionDetails: new MissionDetailsCommand(),
    teamSelection:  new TeamSelectionCommand(),
    missionLaunch:  new MissionLaunchCommand(),
    missionResult:  new MissionResultCommand()
};

export function renderOverlay(type, data) {
    const overlay = document.getElementById("overlay");
    const content = overlay.querySelector(".overlay-content");

    const command = overlayCommands[type] ?? new DefaultCommand();
    content.innerHTML = command.render(data);

    overlay.classList.remove("hidden");

    waitForElement("#closeOverlay", (btn) => {
        btn.addEventListener("click", () => overlay.classList.add("hidden"));
    });
}
