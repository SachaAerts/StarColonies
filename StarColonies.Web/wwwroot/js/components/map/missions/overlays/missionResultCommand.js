import { renderMissionResult } from "./renders/missionResultRender.js";

export class MissionResultCommand {
    render(data) {
        return renderMissionResult(data);
    }
}
