export function parsePlanetData(element) {
    const name = element.dataset.name || "Unknown";
    const image = element.dataset.image || "";
    const x = parseInt(element.dataset.x) || 0;
    const y = parseInt(element.dataset.y) || 0;

    let teams = [];
    try {
        if (element.dataset.teams) {
            teams = JSON.parse(element.dataset.teams);
        }
    } catch (e) {
        console.warn("Failed to parse teams data", e);
    }

    const quests = [...element.querySelectorAll('quest')].map(el => ({
        title: el.getAttribute('title'),
        description: el.getAttribute('description'),
        difficulty: el.getAttribute('difficulty'),
        reward: el.getAttribute('reward'),
        enemies: [...el.querySelectorAll('enemy')].map(e => ({
            name: e.getAttribute('name'),
            image: e.getAttribute('image')
        })),
        rewards: [...el.querySelectorAll('reward')].map(r => ({
            name: r.getAttribute('name'),
            image: r.getAttribute('image'),
            quantity: r.getAttribute('quantity')
        }))
    }));

    return { name, image, x, y, teams, quests };
}
