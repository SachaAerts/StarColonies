export function renderMissionResult(data) {
    console.log("Rendering mission result : ", data);
    return `
        <h4>Résultat de la mission</h4>
        <p>${data.description}</p>

        <p style="color: ${data.isSuccess ? 'lightgreen' : 'crimson'};">
            ${data.isSuccess ? "Mission réussie !" : "Échec de la mission"}
        </p>
        
        ${data.isSuccess ? `
            <h5>Récompenses :</h5>
            <ul>
                ${(data.rewards || []).map(r => `<li> ${r.name}</li>`).join("") || "<li>Aucune</li>"}
            </ul>
        `: ""}
        
        <button id="closeOverlay">Fermer</button>
    `;
}
