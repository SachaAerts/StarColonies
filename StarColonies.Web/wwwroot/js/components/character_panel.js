class CharacterItem extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: 'open' });
    }

    connectedCallback() {
        const imageSrc = this.getAttribute('image') || '';

        this.shadowRoot.innerHTML = `
            <style>
                :host {
                    display: block;
                    position: relative;
                    width: 80px;
                    height: 80px;
                }

                .character {
                    position: absolute;
                    top: 50%; left: 50%;
                    transform: translate(-50%, -50%);
                    height: 70px;
                    transition: 0.2s ease-in-out;
                    cursor: pointer;
                }

                .character:hover {
                    height: 80px;
                }

                svg.case {
                    width: 80px;
                    height: 80px;
                    display: block;
                    transition: all 0.3s ease-in-out;
                }

                svg.case.focused path {
                    fill: #1b2b49;
                    stroke: #3a5a90;
                }
            </style>

            <img class="character" src="${imageSrc}" alt="character">
            <svg class="case" viewBox="0 0 57 57" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M1 11.2667V56H45.5946L56 45.7333V1H11.4054L1 11.2667Z" fill="#111727" stroke="#122743"/>
            </svg>
        `;

        this.characterEl = this.shadowRoot.querySelector('.character');
        this.caseEl = this.shadowRoot.querySelector('.case');

        this.characterEl.addEventListener('click', () => {
            const isFocused = this.caseEl.classList.contains('focused');

            document.querySelectorAll('character-item').forEach(item => {
                item.shadowRoot.querySelector('.case').classList.remove('focused');
            });

            if (!isFocused) {
                this.caseEl.classList.add('focused');
            }
        });
    }
}

class CharacterPanel extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: 'open' });
    }

    connectedCallback() {
        this.shadowRoot.innerHTML = `
            <style>
                .panel {
                    position: relative;
                    font-family: 'Judge', sans-serif;
                    background: rgb(20, 28, 48);
                    border: 2px solid #122743;
                    border-radius:2px;
                    padding: 20px;
                }

                .title h1 {
                    color: rgba(214, 214, 214, 0.193);
                    margin: 0;
                }

                .line {
                    margin-top: 6px; margin-bottom:20px;
                    height: 2px;
                    background-color: rgba(214, 214, 214, 0.193);
                }

                .photos {
                    display: flex;
                    gap: 15px;
                    align-items: center;
                    justify-content: space-around;
                    flex-wrap: wrap;
                    width: 300px;
                }

                .create-button {
                    position:absolute;
                    top:100%; left:50%;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    cursor: pointer;
                    transform:translate(-50%,-50%);
                }
                .create-button .label {
                    z-index: 10;
                    font-size: 1.5rem;
                    color: #EAEAEA;
                }
                .create-button svg {
                    position: absolute;
                    z-index: 5;
                }

            </style>

            <div class="panel">
                <div class="title">
                    <h1>Choose the profil photo</h1>
                    <div class="line"></div>
                </div>
                <div class="photos">
                    <slot></slot>
                </div>
                <div class="create-button">
                    <span class="label">IMPORT</span>
                    <svg width="142" height="25" viewBox="0 0 142 25" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M1 16.4657V8.11111C1 7.88481 1.07676 7.66519 1.21774 7.48816L6.08436 1.37705C6.27409 1.13879 6.56205 1 6.86661 1H133.821C134.107 1 134.379 1.12196 134.568 1.33516L140.063 7.50834C140.226 7.69146 140.316 7.92805 140.316 8.17319V16.4036C140.316 16.6487 140.226 16.8853 140.063 17.0685L134.568 23.2416C134.379 23.4548 134.107 23.5768 133.821 23.5768H6.86662C6.56205 23.5768 6.27409 23.438 6.08436 23.1998L1.21774 17.0886C1.07676 16.9116 1 16.692 1 16.4657Z" fill="#12273B" stroke="#152F49" stroke-width="2"/>
                    </svg>
                </div>
            </div>
        `;
    }
}

customElements.define('character-item', CharacterItem);
customElements.define('character-panel', CharacterPanel);