const availableContainer = document.getElementById('colonistsList');
const teamContainer = document.getElementById('teamList');
let availableColonists = window.availableColonists || [];
let teamColonists = (window.initialTeam && window.initialTeam.length > 0)
    ? window.initialTeam
    : [window.teamOwner];

const jobLabels = {
    0: "Engineer",
    1: "Doctor",
    2: "Scientist",
    3: "Soldier"
};

function getJobLabel(jobId) {
    return jobLabels[jobId] || jobId;
}

function renderColonists(container, colonists, isTeam = false) {
    container.innerHTML = "";

    colonists.forEach((colonist, index) => {
        const div = document.createElement('div');
        const isOwner = colonist.Id === window.teamOwner.Id;
        div.className = isTeam ? 'oneMemberTeamCreateColony cursorPointerCreateColony' : 'oneUserCreateColony cursorPointerCreateColony';
        if (isOwner) div.classList.add("leader");

        if (isTeam) {
            div.innerHTML = `
                ${!isOwner ? `
                    <div class="delete-cross"></div>
                    <div class="delete-indicator"></div>
                ` : ""}
                <img src="/img/upload/${colonist.ProfilPicture}" alt="userLogo"/>
                <div>
                    <h3 class="titlesCreateColony">${colonist.Name}</h3>
                    <p class="textsCreateColony">${getJobLabel(colonist.Job)}</p>
                </div>
            `;
        } else {
            div.innerHTML = `
                <img class="logoUserCreateColony" src="/img/upload/${colonist.ProfilPicture}" alt="logoUser"/>
                <div>
                    <h3 class="titlesCreateColony">${colonist.Name}</h3>
                    <p class="textsCreateColony">Lvl: ${colonist.Level}</p>
                </div>
                <p class="textsCreateColony">${getJobLabel(colonist.Job)}</p>
                <div class="oneUserStatCreateColony">
                    <img src="/img/icons/force.png" alt="strength"/>
                    <p class="textsCreateColony">${colonist.Strength}</p>
                </div>
                <div class="oneUserStatCreateColony">
                    <img src="/img/icons/stamina.png" alt="stamina"/>
                    <p class="textsCreateColony">${colonist.Stamina}</p>
                </div>
            `;
        }

        if (!isOwner) {
            div.addEventListener("click", () => {
                if (isTeam) {
                    availableColonists.push(colonist);
                    teamColonists = teamColonists.filter(c => c.Id !== colonist.Id);
                } else {
                    if (teamColonists.length < 5) {
                        teamColonists.push(colonist);
                        availableColonists = availableColonists.filter(c => c.Id !== colonist.Id);
                    }
                }

                renderColonists(teamContainer, teamColonists, true);
                renderColonists(availableContainer, availableColonists, false);
                updateTeamCount();
            });
        }

        container.appendChild(div);
    });
}

function updateTeamCount() {
    const teamCountElement = document.getElementById('teamCount');
    teamCountElement.textContent = `${teamColonists.length}/5`;
}

renderColonists(availableContainer, availableColonists, false);
renderColonists(teamContainer, teamColonists, true);
updateTeamCount();

document.querySelector("form").addEventListener("submit", () => {
    const nameComponent = document.querySelector("space-input");
    const panel = document.querySelector('character-panel');

    document.getElementById("name-hidden").value = nameComponent.value;

    const { selectedCharacter, uploadedCharacter } = panel.getSelectedImageDetails();
    document.getElementById("character-hidden").value = selectedCharacter || uploadedCharacter || "";

    const colonistsHidden = document.getElementById("colonists-hidden");
    const ids = teamColonists.map(c => c.Id);
    colonistsHidden.value = JSON.stringify(ids);
});