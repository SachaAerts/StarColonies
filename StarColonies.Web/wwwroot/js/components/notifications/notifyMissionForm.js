import { NotificationContext } from "./NotificationContext.js";
import { NotyfStrategy } from "./NotyStrategy.js";

const notifier = new NotificationContext(new NotyfStrategy());

export function limitSelection(select) {
    if (select.selectedOptions.length > 3) {
        notifier.error("Vous ne pouvez sélectionner que 3 monstres maximum.");
        select.options[select.selectedIndex].selected = false;
    }
}

document.addEventListener("DOMContentLoaded", () => {
    const select = document.querySelector("#SelectedEnemyIds");
    if (select) {
        select.addEventListener("change", () => limitSelection(select));
    }
});
