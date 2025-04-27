import { truncateText } from '../../../utils/utils.js';

export function renderMissionFrame(quest, index) {
    return `
        <div class="quest-frame" data-index="${index}">
            <h3>${quest.title}</h3> 
            <p style="font-family: 'Pixelify Sans', sans-serif; font-size: 0.8rem;">
                ${truncateText(quest.description, 100)}
            </p>
            <div style="display: flex; justify-content: space-between; align-items: center;">
                <p>Difficulty : ${quest.difficulty}</p>
                <small>Rewards : ${quest.reward} <img height="20" src="/img/icons/mustysCoin.png" alt="Coins"></small>
            </div>
        </div>
        <style>
            .delete-mission {
                position: absolute; 
                top: 5px; right: 5px; 
                cursor: pointer; 
                z-index: 1000000000;
            }
            .delete-mission:hover {
                transform: scale(0.8);
            }
        </style>
    `;
}