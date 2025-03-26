class Input extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: "open" });
        this.error = true;
    }

    connectedCallback() {
        this.render();
        this.addEventListeners();
    }

    addEventListeners() {
        const input = this.shadowRoot.querySelector("input");
        input.addEventListener("blur", () => this.validateInput());
    }
    validateInput() {
        const input = this.shadowRoot.querySelector("input");
        const errorIcon = this.shadowRoot.querySelector(".error-icon");

        if (this.hasAttribute("required") && !input.value.trim()) {
            input.classList.add("error");
            errorIcon.style.display = "block";
        } else {
            input.classList.remove("error");
            errorIcon.style.display = "none";
        }
    }

    getLabelPosition() {
        return this.getAttribute("label-position") + "%";
    }

    render() {
        this.shadowRoot.innerHTML = `
            <style>
                section {
                    position: relative;
                }
                .label {
                    position: absolute;
                    
                    color: aliceblue;
                    background-color: #161c2b;
                    
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    
                    left: ${this.getLabelPosition()}; top: 0%;
                    padding: 0 10px;

                    white-space: nowrap;
                    min-width: fit-content; 
                    max-width: 100%; 
                    
                    font-size: 1.5rem;
                    
                    transform: translate(-50%,-50%);
                }
                input {
                    position: absolute;
                    top: 50%; left: 50%;

                    background: none;
                    color: rgba(255, 255, 255, 0.647);
                    
                    border: none;
                    border-bottom: 1.5px solid rgba(255, 255, 255, 0.148);
                    
                    font-size: 1.2rem;
                    height: 15px; width: 190px;
                    
                    transform: translate(-50%,-50%);
                    transition: .2s ease-in;
                }
                input:focus-visible {
                    outline: none;
                    border-bottom: 1.5px solid rgb(255, 255, 255);
                }
                input.error {
                    border-bottom: 1.5px solid #43366D;
                }
                .error-icon {
                    position: absolute;
                    left: 99%; top:0%;
                    height: 15px;
                    transform: translate(-50%, -50%);
                    display: none;
                }
            </style>

            <section>
                <div class="label">${this.getAttribute("label") || "Label"}</div>
                <input type="text" ${this.hasAttribute("required") ? "required" : ""}>
                <svg width="270" height="40" viewBox="0 0 270 40" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M1 37.791V20.7239C1 20.4587 1.10536 20.2044 1.29289 20.0168L20.3097 1H21.1791H163.5H178H267.552C268.105 1 268.552 1.44771 268.552 2V20.9925C268.552 21.2577 268.447 21.512 268.259 21.6996L251.461 38.4981C251.273 38.6857 251.019 38.791 250.754 38.791H2C1.44772 38.791 1 38.3433 1 37.791Z" stroke="#152F49" stroke-width="2"/>
                </svg>
                <svg class="error-icon" width="11" height="11" viewBox="0 0 11 11" fill="none" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
                    <circle cx="5.5" cy="5.5" r="5.5" fill="#43366D"/>
                    <rect x="0.333313" y="0.333252" width="10.6667" height="10.6667" fill="url(#pattern0_132_69)"/>
                    <defs>
                        <pattern id="pattern0_132_69" patternContentUnits="objectBoundingBox" width="1" height="1">
                            <use xlink:href="#image0_132_69" transform="scale(0.03125)"/>
                        </pattern>
                        <image id="image0_132_69" width="32" height="32" preserveAspectRatio="none" xlink:href="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAMAAABEpIrGAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAAlQTFRFOAAsi3LeAAAAL3lB/AAAAAN0Uk5T//8A18oNQQAAACxJREFUeNpiYCIAGIaeAkYwGOoKGEDyDDRVwIThhMGogPIEMzx8MSAKAAIMAKsnB6EyR0FYAAAAAElFTkSuQmCC"/>
                    </defs>
                </svg>
            </section>
        `;
    }
}

customElements.define("space-input", Input);

class SelectInput extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: "open" });
    }

    connectedCallback() {
        this.render();
        this.addEventListeners();
    }

    addEventListeners() {
        const select = this.shadowRoot.querySelector("select");
        select.addEventListener("change", () => this.validateSelect());
    }
    validateSelect() {
        const select = this.shadowRoot.querySelector("select");
        const errorIcon = this.shadowRoot.querySelector(".error-icon");

        if (this.hasAttribute("required") && select.value === "default") {
            select.classList.add("error");
            errorIcon.style.display = "block";
        } else {
            select.classList.remove("error");
            errorIcon.style.display = "none";
        }
    }

    getOptions() {
        try {
            return JSON.parse(this.getAttribute("options")) || [];
        } catch {
            return [];
        }
    }

    render() {
        const options = this.getOptions()
            .map(option => `<option value="${option.toLowerCase()}">${option}</option>`)
            .join("");

        this.shadowRoot.innerHTML = `
            <style>
                section {
                    position: relative;
                }
                .label {
                    position: absolute;
                    
                    color: aliceblue;
                    background-color: #161c2b;
                    
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    
                    left: 45%; top: 0%;
                    padding: 0 5px;

                    white-space: nowrap;
                    min-width: fit-content; 
                    max-width: 100%; 
                    
                    font-size: 1.5rem;
                    
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
                .error-icon {
                    position: absolute;
                    left: 99%;
                    top: 50%;
                    height: 15px;
                    transform: translate(-50%, -50%);
                    display: none;
                }
            </style>
            <section class="select">
                <div class="label">${this.getAttribute("label") || "Select an option"}</div>
                <select>
                    <option value="default" disabled selected>Choose...</option>
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