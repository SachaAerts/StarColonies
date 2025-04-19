/**
 * balise : <planet-item ...></planet-item>
 * attributs :
 *  - data-name="<NAME>"
 *  - data-image="<NAME_IMG>"
 *  - data-x="<LEFT_POSITION>"
 *  - data-y="<TOP_POSITION>"
 *  - data-id="<ID:int>"
 * event :
 *  - click -> affichage d'un selecteur sur la planète
 */
class PlanetItem extends HTMLElement {
    constructor() {
        super();
        this.shadow = this.attachShadow({ mode: 'open' });
        this.click = false;
    }

    connectedCallback() {
        this.name  = this.dataset.name    || "Unknown";
        this.image = this.dataset.image   || "";
        console.log(this.image);
        this.x = parseInt(this.dataset.x) || 0;
        this.y = parseInt(this.dataset.y) || 0;
        this.teams = [];

        try {
            if (this.dataset.teams) {
                this.teams = JSON.parse(this.dataset.teams);
            }
        } catch (e) {
            console.warn("Failed to parse teams data", e);
        }
        
        this.quests = [...this.querySelectorAll('quest')].map(el => {
            const enemies = [...el.querySelectorAll('enemy')].map(e => ({
                name: e.getAttribute('name'),
                image: e.getAttribute('image')
            }));

            const rewards = [...el.querySelectorAll('reward')].map(r => ({
                name: r.getAttribute('name'),
                image: r.getAttribute('image'),
                quantity: r.getAttribute('quantity')
            }));

            return {
                title: el.getAttribute('title'),
                description: el.getAttribute('description'),
                difficulty: el.getAttribute('difficulty'),
                reward: el.getAttribute('reward'),
                enemies,
                rewards
            };
        });

        this.innerHTML = '';

        this.render();
    }

    render() {
        this.container = document.createElement('div');
        this.container.classList.add('planet-container');
        this.container.style.top = `${this.y}px`;
        this.container.style.left = `${this.x}px`;

        this.container.addEventListener('click', () => this.handleClick());

        this.shadow.innerHTML = this.getStyle();
        this.shadow.appendChild(this.container);
    }

    getStyle() {
        return `
            <style>
                .planet-container {
                    position: absolute;
                    width: 100px;
                    height: 100px;
                    background-size: cover;
                    background-repeat: no-repeat;
                    background-position: center;
                    background-image: url("${this.dataset.image}");
                    border-radius: 50%;
                    cursor: pointer;
                    z-index: 5000;
                    transition: .5s ease-out;
                }

                .planet-container:active {
                    transform: scale(1.2);
                }

                .selector-svg {
                    position: absolute;
                    top: 50%;
                    left: 50%;
                    transform: translate(-50%, -50%) scale(1.1);
                    animation: shrink-to-fit 0.4s ease-out forwards;
                    pointer-events: none;
                }

                @keyframes shrink-to-fit {
                    from {
                        transform: translate(-50%, -50%) scale(1.2);
                        opacity: 0.2;
                    }
                    to {
                        transform: translate(-50%, -50%) scale(1);
                        opacity: 1;
                    }
                }

                .quest-panel {
                    position: absolute;
                    padding : 10px 5px;
                    
                    width: 244px; height: 330px;
                    
                    background-color: rgba(217, 217, 217, 0.06);
                    
                    border: 2px solid #152F49;
                    border-radius: 6px;
                    
                    z-index: 9999;

                    transform-origin: center center;
                    transform: scaleY(0);
                    animation: panel-grow 0.3s ease-out forwards;
                }

                @keyframes panel-grow {
                    0% {
                        transform: scaleY(0);
                        opacity: 0;
                    }
                    100% {
                        transform: scaleY(1);
                        opacity: 1;
                    }
                }
                
                @keyframes panel-shrink {
                    0% {
                        transform: scaleY(1);
                        opacity: 1;
                    }
                    20% {
                        transform: scaleY(0.8);
                        opacity: 1;
                    }
                    40% {
                        transform: scaleY(0.6);
                        opacity: 1;
                    }
                    60% {
                        transform: scaleY(0.4);
                        opacity: 1;
                    }
                    80% {
                        transform: scaleY(0.2);
                        opacity: 1;
                    }
                    90% {
                        transform: scaleY(0.1);
                        opacity: 1;
                    }
                    100% {
                        transform: scaleY(0);
                        opacity: 0;
                    }
                }  

                .quest-scroll::-webkit-scrollbar {
                    width: 6px;
                }

                .quest-scroll::-webkit-scrollbar-track {
                    background: transparent;
                }

                .quest-scroll::-webkit-scrollbar-thumb {
                    background-color: #152F49;
                    border-radius: 10px;
                }

                .quest-scroll.none {
                    display:none;
                }
                .quest-scroll {
                    position: relative;
                    height: 100%; width: 100%;
                    overflow-y: auto;
                    scrollbar-width: thin;
                    scrollbar-color: #152F49 transparent;
                    display: flex;
                    flex-direction: column;
                    gap: 15px;
                }
                
                .quest-frame {
                    color: rgba(255,255,255,0.66);
                    
                    font-family: 'Judge', sans-serif;
                    
                    background: #152F49;
                    
                    border: 2px solid rgba(36,77,117,0.63);
                    border-radius: 6px;
                    
                    padding: 10px;
                    
                    transition: all .2s ease-out;
                }
                
                .quest-frame:hover {
                    transform: scale(0.95);
                }
                
                .quest-frame h3 {
                    font-family: 'Judge Italic', sans-serif;
                    padding: 0; margin: 0; 
                }
                
                small {
                    display: flex;
                    align-items: center;
                    justify-content: end;
                }
            </style>
        `;
    }

    handleClick() {
        if (this.click) {
            const selector = this.shadow.querySelector('.selector-svg');
            const panel = this.shadow.querySelector('.quest-panel');

            selector?.remove();

            if (panel) {
                panel.style.animation = 'panel-shrink 0.3s ease-out forwards';
                panel.addEventListener('animationend', () => {
                    panel.remove();
                }, { once: true });
            }

            this.click = false;
            return;
        }

        document.querySelectorAll('planet-item').forEach(item => {
            const shadow = item.shadowRoot;
            const panel = shadow?.querySelector('.quest-panel');
            const selector = shadow?.querySelector('.selector-svg');

            selector?.remove();

            if (panel) {
                panel.style.animation = 'panel-shrink 0.3s ease-out forwards';
                panel.addEventListener('animationend', () => {
                    panel.remove();
                }, { once: true });
            }

            item.click = false;
        });

        this.container.insertAdjacentHTML('beforeend', `
            <svg class="selector-svg" viewBox="0 0 199 199" xmlns="http://www.w3.org/2000/svg">
                <path d="M1 130.5V198H69M1 68.5V1H69M198 130.5V198H130M198 68.5V1H130"
                      stroke="#E1F7FF" stroke-width="2.5"/>
            </svg>
        `);

        this.showQuestPanel();
        this.click = true;
    }

    truncateText(text, maxLength) {
        if (!text) return '';
        return text.length > maxLength ? text.substring(0, maxLength).trim() + "..." : text;
    }

    showQuestPanel() {
        const panel = document.createElement('div');
        panel.classList.add('quest-panel');

        const openRight = this.x < 1600;
        panel.style.left = openRight ? '110px' : '-260px';

        const scrollContainer = document.createElement('div');
        scrollContainer.classList.add('quest-scroll');
        scrollContainer.innerHTML = this.generateQuestFrames();

        panel.appendChild(scrollContainer);
        this.container.appendChild(panel);
        this.overlay(panel);
    }

    overlay(panel) {
        panel.querySelectorAll('.quest-frame').forEach((el, index) => {
            el.addEventListener('click', (event) => {
                event.stopPropagation();

                const quest = this.quests[index];

                renderOverlay("missionDetails", quest);

                setTimeout(() => {
                    const launchBtn = document.querySelector("#launchMission");

                    if (launchBtn) {
                        launchBtn.addEventListener("click", () => {
                            const data = {
                                teams: this.teams,
                                items: [
                                    { id: 1, name: "Kit de soins", image: "/img/items/medkit.png" },
                                    { id: 2, name: "Amplificateur", image: "/img/items/booster.png" }
                                ]
                            };

                            renderOverlay("teamSelection", data);
                            this.missionLaunch()
                            
                        });
                    }
                }, 50);
            });
        });
    }
    
    missionLaunch() {
        setTimeout(() => {
            const confirmBtn = document.querySelector("#confirmLaunch");

            if (confirmBtn) {
                confirmBtn.addEventListener("click", () => {
                    const selectedTeam = document.getElementById("teamSelect")?.value;

                    const selectedItems = Array.from(document.querySelectorAll('input[type="checkbox"]:checked'))
                        .map(input => parseInt(input.value));

                    renderOverlay("missionLaunch", {
                        planetImg: this.image,
                        teamId: selectedTeam,
                        items: selectedItems
                    });
                });
            }
        }, 50);
    }
    
    generateQuestFrames() {
        return this.quests.map((quest, i) => `
        <div class="quest-frame" data-index="${i}">
            <h3>${quest.title}</h3>
            <p style="font-family: 'Pixelify Sans', sans-serif;font-size: 0.8rem">
                ${this.truncateText(quest.description, 100)}
            </p>
            <div style="display: flex; justify-content: space-between; align-items: center;">
                <p style="padding: 0; margin: 0;">Difficulty : ${quest.difficulty}</p>
                <small style="padding: 0; margin: 0;">Rewards : ${quest.reward}<img height="20" src="/img/icons/mustysCoin.png" alt="Coins"></small>
            </div>
        </div>
    `).join('');
    }

}

customElements.define('planet-item', PlanetItem);