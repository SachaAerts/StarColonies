
/**
 * Using : <stat-panel force="0" stamina="0" max="7"></stat-panel>
 */
class StatPanel extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: 'open' });

        this.stats = {
            force: parseInt(this.getAttribute('force')) || 0,
            stamina: parseInt(this.getAttribute('stamina')) || 0
        };
        this.max = parseInt(this.getAttribute('max')) || 7;
    }

    connectedCallback() {
        this.render();
        this.updateButtons();
    }

    getTotalAllocated() {
        return this.stats.force + this.stats.stamina;
    }

    updateButtons() {
        const remaining = this.max - this.getTotalAllocated();
        this.shadowRoot.querySelector('.stat-max p').textContent = remaining;

        ['force', 'stamina'].forEach(stat => {
            const moreBtn = this.shadowRoot.querySelector(`.${stat} .more`);
            moreBtn.disabled = remaining === 0;
        });
    }

    updateStat(stat, delta) {
        const newValue = this.stats[stat] + delta;
        if (newValue < 0 || this.getTotalAllocated() + delta > this.max) return;
        this.stats[stat] = newValue;
        this.render();
        this.updateButtons();
    }

    render() {
        this.shadowRoot.innerHTML = `
            <style>
                ${this.getStyles()}
            </style>
            <section class="stats-panel">
                <div class="stats-container">
                    ${this.renderStat('force', 'img/icons/force.png')}
                    ${this.renderStat('stamina', 'img/icons/stamina.png')}
                </div>
                <div class="stat-max">
                    <img src="img/icons/lvl.png" alt="">
                    <p>${this.max - this.getTotalAllocated()}</p>
                </div>
            </section>
        `;

        ['force', 'stamina'].forEach(stat => {
            this.shadowRoot.querySelector(`.${stat} .more`).addEventListener('click', () => this.updateStat(stat, 1));
            this.shadowRoot.querySelector(`.${stat} .minus`).addEventListener('click', () => this.updateStat(stat, -1));
        });
    }

    renderStat(name, icon) {
        return `
            <div class="stat ${name}">
                <div class="type">
                    <img src="${icon}" class="stat-img" alt="">
                    <p>${name.charAt(0).toUpperCase() + name.slice(1)}</p>
                </div>
                <div class="modifier">
                    <img src="img/icons/modifier.png" class="minus" alt="" draggable="false">
                    <p>${this.stats[name]}</p>
                    <img src="img/icons/modifier.png" class="more" alt="" draggable="false">
                </div>
            </div>
        `;
    }

    getStyles() {
        return `
            .stats-panel {
                user-select: none;
                -webkit-user-drag: none;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                height:180px;
                position:relative;
                display: flex;
                justify-content: center;
                align-items: center;
                flex-direction: column;
                color: #FFFFFF;
                gap: 20px;
                border:2px solid #152F49;
                border-radius:3px;
                padding:0 40px;
            }

            .stats-container {
                display: flex;
                gap: 50px;
            }

            .stat-max {
                position:absolute;
                top:70%; left:10px;
                display: flex;
                justify-content: center;
                align-items: center;
                gap: 5px;
                height: 50px; width: 50px;
            }

            .stat {
                display: flex;
                gap: 20px;
            }

            .stat-img {
                height: 50px;
            }

            .stat .type {
                display: flex;
                flex-direction: column;
                justify-content: center;
                align-items: center;
                gap: 0px;
            }

            .stat .type p {
                margin: 0;
            }

            .stat .modifier {
                display: flex;
                align-items: center;
                justify-content: center;
                gap: 10px;
            }

            .stat .modifier img {
                height: 30px;
                cursor: pointer;
            }

            .stat .modifier img.minus:active {
                transform: scale(1.2);
            }

            .stat .modifier img.more {
                transform: rotate(180deg);
            }

            .stat .modifier img.more:active {
                transform: scale(1.2) rotate(180deg);
            }

            .stat .modifier img:disabled {
                opacity: 0.5;
                pointer-events: none;
            }
        `;
    }
}

customElements.define('stat-panel', StatPanel);
