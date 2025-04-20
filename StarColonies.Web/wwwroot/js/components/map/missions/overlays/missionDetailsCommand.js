import { renderMissionDetails } from "./renders/missionDetailsRender.js";

export class MissionDetailsCommand {
    render(data) {
        return renderMissionDetails(data);
    }
}