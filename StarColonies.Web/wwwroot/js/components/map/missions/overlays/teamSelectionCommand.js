import { renderTeamSelection } from "./renders/teamSelectionRender.js";

export class TeamSelectionCommand {
    render(data) {
        return renderTeamSelection(data);
    }
}
