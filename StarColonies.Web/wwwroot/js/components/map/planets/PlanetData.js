export function parsePlanetData(element) {
    return {
        name: element.getAttribute("data-name"),
        image: element.getAttribute("data-image"),
        x: parseInt(element.getAttribute("data-x")),
        y: parseInt(element.getAttribute("data-y")),
        teams: JSON.parse(element.getAttribute("data-teams")),
        quests: Array.from(element.querySelectorAll("quest")).map(q => ({
            title: q.getAttribute("title"),
            description: q.getAttribute("description"),
            difficulty: q.getAttribute("difficulty"),
            reward: q.getAttribute("reward"),
            enemies: Array.from(q.querySelectorAll("enemy")).map(e => ({
                name: e.getAttribute("name"),
                image: e.getAttribute("image")
            })),
            rewards: Array.from(q.querySelectorAll("reward")).map(r => ({
                name: r.getAttribute("name"),
                image: r.getAttribute("image")
            }))
        })),
        items: JSON.parse(element.getAttribute("data-items"))
    };
}
