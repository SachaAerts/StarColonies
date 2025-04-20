import { renderLaunchLoading } from "./renders/missionLaunchRender.js";

export class MissionLaunchCommand {
    render(data) {
        return renderLaunchLoading(data.planetImg);
    }
}
