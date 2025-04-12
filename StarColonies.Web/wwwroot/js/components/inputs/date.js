class DateInput extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: "open" });
    }

    connectedCallback() {
        this.render();
        this.addEventListeners();
    }

    getLabelPosition() {
        return this.getAttribute("label-position") + "%" || "30%";
    }

    getDateFormat() {
        return this.getAttribute("format") || "dd/mm/yyyy";
    }

    validateDate() {
        const input = this.shadowRoot.querySelector("input");
        const errorIcon = this.shadowRoot.querySelector(".error-icon");
        const errorMessage = this.shadowRoot.querySelector(".error-message");

        const value = input.value.trim();
        const required = this.hasAttribute("required");
        const format = this.getDateFormat();
        const formatRegex = this.generateRegexFromFormat(format);

        const isValidFormat = formatRegex.test(value);
        let isValidDate = false;

        if (isValidFormat) {
            const separator = format.includes("/") ? "/" : "-";
            const parts = value.split(separator);
            const formatParts = format.split(separator);

            let day, month, year;
            for (let i = 0; i < formatParts.length; i++) {
                if (formatParts[i] === "dd") day = parseInt(parts[i], 10);
                if (formatParts[i] === "mm") month = parseInt(parts[i], 10) - 1;
                if (formatParts[i] === "yyyy") year = parseInt(parts[i], 10);
            }

            const date = new Date(year, month, day);
            isValidDate = (
                date.getFullYear() === year &&
                date.getMonth() === month &&
                date.getDate() === day
            );
        }

        const isEmpty = required && !value;
        const hasError = isEmpty || !isValidFormat || !isValidDate;

        if (hasError) {
            input.classList.add("error");
            errorIcon.style.display = "block";
            errorMessage.style.display = "block";

            if (isEmpty) {
                errorMessage.textContent = "Ce champ est requis.";
            } else if (!isValidFormat) {
                errorMessage.textContent = `Format attendu : ${format}`;
            } else if (!isValidDate) {
                errorMessage.textContent = "Date invalide.";
            }
        } else {
            input.classList.remove("error");
            errorIcon.style.display = "none";
            errorMessage.style.display = "none";
        }
    }

    generateRegexFromFormat(format) {
        const separator = format.includes("/") ? "/" : "-";

        const escapedSeparator = separator === "/" ? "\\/" : "-";

        const regexStr = format
            .replace("dd", "(\\d{2})")
            .replace("mm", "(\\d{2})")
            .replace("yyyy", "(\\d{4})")
            .replaceAll(separator, escapedSeparator);

        return new RegExp("^" + regexStr + "$");
    }

    onTyping(e) {
        const input = e.target;
        const raw = input.value.replace(/\D/g, '');
        const { separator, blocks } = this.getFormatBlocksAndSeparator();

        let value = '';
        let index = 0;

        for (let i = 0; i < blocks.length; i++) {
            const blockLength = blocks[i];
            const nextChunk = raw.slice(index, index + blockLength);
            if (!nextChunk) break;

            value += nextChunk;
            index += blockLength;

            if (i < blocks.length - 1 && nextChunk.length === blockLength) {
                value += separator;
            }
        }

        input.value = value;
    }

    getFormatBlocksAndSeparator() {
        const format = this.getDateFormat();
        const separator = format.includes("/") ? "/" : "-";
        const blocks = format.split(separator).map(block => {
            if (block === "dd" || block === "mm") return 2;
            if (block === "yyyy") return 4;
            return block.length;
        });
        return { separator, blocks };
    }

    addEventListeners() {
        const input = this.shadowRoot.querySelector("input");
        input.addEventListener("blur", () => this.validateDate());
        input.addEventListener("input", (e) => this.onTyping(e));
    }

    get value() {
        const input = this.shadowRoot.querySelector("input");
        return input ? input.value : "";
    }

    render() {
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
                    left: ${this.getLabelPosition()};
                    top: 0%;
                    padding: 0 10px;
                    font-size: 1.5rem;
                    font-family: 'Judge', sans-serif;
                    transform: translate(-50%,-50%);
                    white-space: nowrap;
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
                    left: 99%;
                    top: 0%;
                    height: 15px;
                    transform: translate(-50%, -50%);
                    display: none;
                }                    
                .error-message {
                    position: absolute;
                    top: 100%;
                    left: 50%;
                    transform: translate(-50%, 0);
                    font-size: 0.9rem;
                    color: #43366D;
                    white-space: nowrap;
                }
            </style>
            <section>
                <div class="label">${this.getAttribute("label") || "Date"}</div>
                <input type="text" placeholder="${this.getDateFormat()}" ${this.hasAttribute("required") ? "required" : ""}>
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
                <div class="error-message" style="display:none;"></div>
            </section>
        `;
    }
}

customElements.define("space-date", DateInput);
