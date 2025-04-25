import { NotificationContext } from "./NotificationContext.js";
import { NotyfStrategy } from "./NotyStrategy.js";

const notifier = new NotificationContext(new NotyfStrategy());

export function limitSelection(select) {
    if (select.selectedOptions.length > 3) {
        notifier.error("Vous ne pouvez sélectionner que 3 monstres maximum.");
        select.options[select.selectedIndex].selected = false;
    }
}

export function limitCoins(coins) {
    const min = 1, max = 1000;
    if (coins < min || coins > max) {
        notifier.error(`The number of coins must be between ${min} and ${max}.`);
    }
}

export function limitText(texte, max) {
    if (texte.length > max) {
        notifier.error(`The text must be less than ${max} characters.`);
    }
}

document.addEventListener("DOMContentLoaded", () => {
    const select = document.querySelector("#SelectedEnemyIds");
    if (select) {
        select.addEventListener("change", () => limitSelection(select));
    }
});

document.addEventListener("DOMContentLoaded", () => {
    const coinsInput = document.querySelector("#coinsInput");
    if (coinsInput) {
        coinsInput.addEventListener("input", () => {
            const coins = parseInt(coinsInput.value, 10);
            limitCoins(coins);
        });
    }
});

document.addEventListener("DOMContentLoaded", () => {
    const descriptionInput = document.querySelector("#descriptionInput");
    const titleInput = document.querySelector("#titleInput");
    if (descriptionInput) {
        descriptionInput.addEventListener("input", () => {
            const text = descriptionInput.value;
            limitText(text, 500);
        });
    }
    if (titleInput) {
        titleInput.addEventListener("input", () => {
            const title = titleInput.value;
            limitText(title, 100);
        });
    }
});