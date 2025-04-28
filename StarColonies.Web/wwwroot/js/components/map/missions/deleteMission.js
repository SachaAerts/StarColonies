import { NotificationContext } from '../../notifications/NotificationContext.js'
import { NotyfStrategy } from '../../notifications/NotyStrategy.js'

const notify = new NotificationContext(new NotyfStrategy());

export function setupToggleVisibilityListener(id) {
    document.querySelectorAll('.toggle-visibility input').forEach(input => {
        input.addEventListener('change', async (e) => {
            const missionId = e.target.closest('.toggle-visibility').dataset.id;

            await onPost(missionId);

            window.location.reload();
        });
    });
}


async function onPost(missionId) {
    const BASE_PATH = window.BASE_PATH || "";
    const url = BASE_PATH + `/DeleteMission/${missionId}`;
    
    const response = await fetch(url, {
        method: "POST",
        headers: {"Accept": "application/json", "Content-Type": "application/json"}
    });

    const result = await response.json();

    if (result.success) {
        location.reload();
    } else notify.error("Erreur lors de la suppression !");
}
