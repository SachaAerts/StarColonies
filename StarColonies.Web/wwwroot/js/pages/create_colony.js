const availableContainer = document.getElementById('colonistsList');
const teamContainer = document.getElementById('teamList');
let availableColonists = window.availableColonists || [];
let teamColonists = [];

const jobLabels = {
    0: "Engineer",
    1: "Doctor",
    2: "Scientist",
    3: "Soldier"
};

function getJobLabel(jobId) {
    return jobLabels[jobId] || jobId ;
}

function renderColonists(container, colonists, isTeam = false) {
    container.innerHTML = "";

    colonists.forEach(colonist => {
        const div = document.createElement('div');
        div.className = isTeam ? 'oneMemberTeamCreateColony' : 'oneUserCreateColony';

        if (isTeam) {
            // STRUCTURE : .oneMemberTeamCreateColony
            div.innerHTML = `
                <div class="delete-cross"></div>
                <div class="delete-indicator"></div>
                <img src="/img/upload/${colonist.ProfilPicture}" alt="userLogo"/>
                <div>
                    <h3 class="titlesCreateColony">${colonist.Name}</h3>
                    <p class="textsCreateColony">${jobLabels[colonist.Job] || colonist.Job}</p>
                </div>
            `;
        } else {
            div.innerHTML = `
                <img class="logoUserCreateColony" src="/img/upload/${colonist.ProfilPicture}" alt="logoUser"/>
                <div>
                    <h3 class="titlesCreateColony">${colonist.Name}</h3>
                    <p class="textsCreateColony">Lvl: ${colonist.Level}</p>
                </div>
                <p class="textsCreateColony">${jobLabels[colonist.Job] || colonist.Job}</p>
                <div class="oneUserStatCreateColony">
                    <img src="/img/icons/force.png" alt="strengh"/>
                    <p class="textsCreateColony">${colonist.Strength}</p>
                </div>
                <div class="oneUserStatCreateColony">
                    <img src="/img/icons/stamina.png" alt="stamina"/>
                    <p class="textsCreateColony">${colonist.Stamina}</p>
                </div>
            `;
        }

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
        });

        container.appendChild(div);
    });
}

renderColonists(availableContainer, availableColonists, false);
renderColonists(teamContainer, teamColonists, true);