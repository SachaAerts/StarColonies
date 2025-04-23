export function renderMissionResult(data) {

    const notyf = new Notyf({
        duration: 8000,
        ripple: true,
        position: { x: 'right', y: 'top' }
    });

    const livingColony = data.livingColony;
    const overcomingMission = data.overcomingMission;
    const isSuccess = livingColony && overcomingMission;

    if (isSuccess) {
        notyf.success("Mission réussie !");
        notyf.success("Tous les membres de l’équipe gagnent un niveau !");
        if (data.coinsReward > 0) {
            notyf.success(`Vous gagnez ${data.coinsReward} Musty !`);
        }
        if (data.rewards && data.rewards.length > 0) {
            data.rewards.forEach(r => notyf.success(`Ressource obtenue : ${r.name}`));
        }
    } else if (overcomingMission && !livingColony) {
        notyf.error("Les défis ont été surmontés, mais l’équipe n’a pas survécu.");
    } else if (!overcomingMission && livingColony) {
        notyf.error("L’équipe a survécu, mais n’a pas réussi à surmonter les défis.");
        notyf.success("Tous les membres de l’équipe gagnent un niveau !");
    } else {
        notyf.error("Échec complet : l’équipe n’a pas survécu et les défis n’ont pas été surmontés.");
    }

    return `
        <style>
            .notyf {
                z-index: 99999;
            }
        </style>
        <h4>Résultat de la mission</h4>
        <p>${isSuccess ? "Mission réussie !" : "Échec de la mission"}</p>

        <p style="color: ${isSuccess ? 'lightgreen' : 'crimson'};">
            ${data.description}
        </p>

        ${isSuccess ? `
            <h5>Récompenses :</h5>
            <ul>
                ${(data.rewards || []).map(r => `<li>${r.name}</li>`).join("") || "<li>Aucune</li>"}
            </ul>
            <p><strong>Musty gagnés :</strong> ${data.coinsReward}</p>
        ` : `
            <h5>Pas de récompense : l’équipe ne gagne rien.</h5>
        `}
        
        <button id="closeOverlay">Fermer</button>
    `;
}
