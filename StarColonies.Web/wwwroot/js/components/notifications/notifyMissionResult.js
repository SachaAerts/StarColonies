
import { NotificationContext } from "./NotificationContext.js";
import { NotyfStrategy } from "./NotyStrategy.js";

const notifier = new NotificationContext(new NotyfStrategy());

export const notifyMissionResult = (result, mission) => {
    const livingColony = result.livingColony;
    const overcomingMission = result.overcomingMission;
    const isSuccess = livingColony && overcomingMission;
    
    let message = "Complete failure: the team did not survive and the challenges were not overcome.";
    
    if (isSuccess) {
        message = "Successful mission !";
        notifier.success(message);
        notifierLevelUp();
        notifierMoney(mission.coinsReward);
        notifierItems(mission.items);
    } else if (overcomingMission && !livingColony) {
        message = "The challenges were overcome, but the team did not survive.";
        notifier.error(message);
        notifierMoney(mission.coinsReward);
        notifierItems(mission.items);
    } else if (!overcomingMission && livingColony) {
        message = "The team survived, but failed to overcome the challenges.";
        notifier.error(message);
        notifierLevelUp();
    } else notifier.error("Complete failure: the team did not survive and the challenges were not overcome.");
    
    return message;
}

function notifierMoney(coinsReward) {
    if (coinsReward > 0) notifier.success(`You get ${coinsReward} Musty !`);
}

function notifierItems(items) {
    items?.forEach(r => notifier.success(`Resource obtained : ${r.item.name}`));
}

function notifierLevelUp() {
    notifier.success("All team members gain a level !");
}