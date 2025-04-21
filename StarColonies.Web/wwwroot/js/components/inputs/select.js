class SelectInput extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: "open" });
    }

    connectedCallback() {
        this.render();
    }

    getOptions() {
        try {
            return JSON.parse(this.getAttribute("options")) || [];
        } catch {
            return [];
        }
    }

    get value() {
        const select = this.shadowRoot.querySelector("select")
        return select ? select.value : ""
    }

    get defaultValue() {
        return this.getAttribute("value") || "";
    }

    set value(val) {
        const select = this.shadowRoot.querySelector("select");
        if (select) select.value = val;
    }

    render() {
        const defaultValue = this.defaultValue;
        const options = this.getOptions()
            .map(option => {
                const selected = option === defaultValue ? "selected" : "";
                return `<option value="${option}" ${selected}>${option}</option>`;
            })
            .join("");

        this.shadowRoot.innerHTML = `
            <style>
                section {
                    position: relative;
                    height:40px; width:270px;
                }
                .label {
                    position: absolute;
                    
                    color: aliceblue;
                    background-color: #161c2b;
                    
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    
                    left: 50%; top: 0%;
                    padding: 0 5px;

                    white-space: nowrap;
                    min-width: fit-content; 
                    max-width: 100%; 
                    
                    font-size: 1.5rem;
                    font-family: 'Judge', sans-serif;
                    
                    transform: translate(-50%,-50%);
                }
                select {
                    position: absolute;
                    background: none;
                    border: none;
                    height: 15px; width: 190px;
                    top: 50%; left: 50%;
                    transform: translate(-50%,-50%);
                    transition: .2s ease-in;
                    color: rgba(255, 255, 255, 0.647);
                    font-size: 1.2rem;
                    font-family: 'Judge', sans-serif;
                }
                select option {
                    background: #152f49;
                    color: white;
                    font-size: 16px;
                    border-radius: 0; 
                }
                select:focus-visible {
                    outline: none;
                }
                select.error {
                    border-bottom: 1.5px solid #43366D;
                }
            </style>
            <section class="select">
                <div class="label">${this.getAttribute("label") || "Select an option"}</div>
                <select>
                    <option value="" disabled ${!this.defaultValue ? "selected" : ""}>Choose...</option>
                    ${options}
                </select>
                <svg width="270" height="40" viewBox="0 0 270 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M1 37.791V20.7239C1 20.4587 1.10536 20.2044 1.29289 20.0168L20.3097 1H21.1791H163.5H178H267.552C268.105 1 268.552 1.44771 268.552 2V20.9925C268.552 21.2577 268.447 21.512 268.259 21.6996L251.461 38.4981C251.273 38.6857 251.019 38.791 250.754 38.791H2C1.44772 38.791 1 38.3433 1 37.791Z" stroke="#152F49" stroke-width="2"/>
                </svg>
            </section>

        `;
    }
}

customElements.define("space-select", SelectInput);
