class AstronautComponent {
    constructor(container) {
        this.container = document.querySelector(container);
        this.createAstronaut();
    }

    createAstronaut() {
        const astronaut = document.createElement("div");
        astronaut.classList.add("astronaut");

        astronaut.innerHTML = `
            <div class="head"></div>
            <div class="arm arm-left"></div>
            <div class="arm arm-right"></div>
            <div class="body">
                <div class="panel"></div>
            </div>
            <div class="leg leg-left"></div>
            <div class="leg leg-right"></div>
            <div class="schoolbag"></div>
        `;

        this.container.appendChild(astronaut);
    }
}

document.addEventListener("DOMContentLoaded", () => {
    new AstronautComponent("body");
});
