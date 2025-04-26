import { NotificationContext } from '../../notifications/NotificationContext.js'
import { NotyfStrategy } from '../../notifications/NotyStrategy.js'

const notify = new NotificationContext(new NotyfStrategy());

export function setupDeleteListener(id) {
    document.addEventListener("click", async (event) => {
        const deleteButton = event.target.closest(".delete-mission");

        if (deleteButton) {
            console.log("Mission ID:", id);
            const questFrame = event.target.closest(".quest-frame");

            if (confirm("Would you like to delete this mission?")) {
                try { await onPost(id, questFrame); } 
                catch (error) { notify.error("Erreur de communication avec le serveur."); }
            }
        }
    });
}

async function onPost(missionId, questFrame) {
    const response = await fetch(`/DeleteMission/${missionId}`, {
        method: "POST",
        headers: {"Accept": "application/json", "Content-Type": "application/json"}
    });

    const result = await response.json();
    console.log("Response:", result);

    if (result.success) {
        console.log("Mission deleted successfully");

        if (questFrame) {
            questFrame.remove();
        }
        
        location.reload();
    } else {
        console.log("Error deleting mission");
        notify.error("Erreur lors de la suppression !");
    }
}
