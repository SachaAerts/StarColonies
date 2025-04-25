export function parsePlanetData(element) {
    return {
        id: element.dataset.id,
        name: element.dataset.name,
        image: element.dataset.image,
        x: parseInt(element.dataset.x), y: parseInt(element.dataset.y),
        teams: parseJsonAttribute(element.dataset.teams),
        items: parseJsonAttribute(element.dataset.items),
        quests: parseQuests(element)
    };
}

function parseJsonAttribute(attr) {
    try {
        return JSON.parse(attr);
    } catch (error) {
        console.error("Erreur lors du parsing JSON :", attr, error);
        return [];
    }
}

function parseQuests(element) {
    return Array.from(element.querySelectorAll("quest")).map(parseQuest);
}

function parseQuest(q) {
    return {
        id: q.getAttribute("id"),
        title: q.getAttribute("title"),
        description: q.getAttribute("description"),
        difficulty: q.getAttribute("difficulty"),
        reward: q.getAttribute("reward"),
        enemies: parseEnemies(q),
        rewards: parseRewards(q)
    };
}

function parseEnemies(questElement) {
    return Array.from(questElement.querySelectorAll("enemy")).map(e => ({
        name: e.getAttribute("name"),
        image: e.getAttribute("image")
    }));
}

function parseRewards(questElement) {
    return Array.from(questElement.querySelectorAll("reward")).map(r => ({
        name: r.getAttribute("name"),
        image: r.getAttribute("image"),
        quantity: r.getAttribute("quantity")
    }));
}