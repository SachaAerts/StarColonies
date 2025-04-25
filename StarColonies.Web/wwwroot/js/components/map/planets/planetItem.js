import { parsePlanetData } from './planetData.js';
import { createPlanetContainer } from './planetRenderer.js';
import { getStyle } from './planetStyle.js';
import { createQuestPanel } from '../missions/missionPanel.js';

export class PlanetItem extends HTMLElement {
    
    constructor() {
        super();
        this.shadow = this.attachShadow({ mode: 'open' });
        this.click = false;
    }
    
    connectedCallback() {
        const {id, name, image, x, y, teams, items, quests } = parsePlanetData(this);
        this.id = id;
        this.name = name;
        this.image = image;
        this.items = items;
        
        this.x = x; this.y = y;
        
        this.teams = teams;
        this.quests = quests;
        
        this.innerHTML = '';
        
        this.container = createPlanetContainer(this.x, this.y, this.image, () => this.handleClick());
        this.shadow.innerHTML = getStyle(this.image);
        this.shadow.appendChild(this.container);
    }

    handleClick() {
        const closeAll = () => {
            document.querySelectorAll('planet-item').forEach(item => {
                const shadow = item.shadowRoot;
                shadow?.querySelector('.selector-svg')?.remove();
                const panel = shadow?.querySelector('.quest-panel');
                if (panel) {
                    panel.style.animation = 'panel-shrink 0.3s ease-out forwards';
                    panel.addEventListener('animationend', () => panel.remove(), { once: true });
                }
                item.click = false;
                item.removeAttribute("selected");
            });
        };

        if (this.click) {
            if (!event.target.closest('.quest-panel')) {
                closeAll();
            }
            return;
        }

        closeAll();

        this.container.insertAdjacentHTML('beforeend', `
            <svg class="selector-svg" viewBox="0 0 199 199" xmlns="http://www.w3.org/2000/svg">
                <path d="M1 130.5V198H69M1 68.5V1H69M198 130.5V198H130M198 68.5V1H130"
                      stroke="#E1F7FF" stroke-width="2.5"/>
            </svg>
        `);

        const panel = createQuestPanel(this);
        this.container.appendChild(panel);
        this.click = true;
        this.setAttribute("selected", "true");

        panel.addEventListener('click', (e) => {
            e.stopPropagation();
        });
    }
}

customElements.define('planet-item', PlanetItem);
