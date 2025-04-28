
import { NotificationContext } from "./NotificationContext.js";
import { NotyfStrategy } from "./NotyStrategy.js";

const notifier = new NotificationContext(new NotyfStrategy());

export const notifyMissionResult = (result, mission) => {
    const livingColony = result.livingColony;
    const overcomingMission = result.overcomingMission;
    const isSuccess = livingColony && overcomingMission;
    let message = "Échec complet : l’équipe n’a pas survécu et les défis n’ont pas été surmontés.";
    if (isSuccess) {
        message = "Mission réussie !";
        notifier.success(message);
        notifier.success("Tous les membres de l’équipe gagnent un niveau !");
        if (result.coinsReward > 0) {
            notifier.success(`Vous gagnez ${mission.coinsReward} Musty !`);
        }
        mission.items?.forEach(r => notifier.success(`Ressource obtenue : ${r.item.name}`));
    } else if (overcomingMission && !livingColony) {
        message = "Les défis ont été surmontés, mais l’équipe n’a pas survécu.";
        notifier.error(message);
        if (result.coinsReward > 0) notifier.success(`Vous gagnez ${mission.coinsReward} Musty !`);
    } else if (!overcomingMission && livingColony) {
        message = "L’équipe a survécu, mais n’a pas réussi à surmonter les défis.";
        notifier.error(message);
        notifier.success("Tous les membres de l’équipe gagnent un niveau !");
    } else notifier.error("Échec complet : l’équipe n’a pas survécu et les défis n’ont pas été surmontés.");
    return message;
}