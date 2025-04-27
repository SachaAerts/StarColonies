using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;
using StarColonies.Infrastructures.Data.Seeder.Factories;

namespace StarColonies.Infrastructures.Data.Seeder.Regiters;

public class MissionRegister
{
    public static List<MissionEntity> Register(IList<PlanetEntity> planets, IList<EnemyEntity> enemies,
        List<ItemEntity> items)
    {
        return new List<MissionEntity>
        {
            MissionFactory.Create(
                name: "Exploration de ruines",
                description:
                $"Les restes d’une ancienne civilisation extraterrestre ont été découverts sur {planets[0].Name}. " +
                "Votre mission est de fouiller ces ruines mystérieuses pour récupérer des artefacts technologiques enfouis.",
                coins: 10,
                planet: planets[0],
                enemies: SelectEnemies(enemies, [0, 1, 2]),
                items: SelectRewards(1, items, [5, 1])
            ),

            MissionFactory.Create(
                name: "Survie nocturne",
                description: $"Une nuit éternelle s’est abattue sur {planets[1].Name}. " +
                             "Votre équipe doit survivre 48 heures dans un environnement hostile et obscur.",
                coins: 3,
                planet: planets[1],
                enemies: SelectEnemies(enemies, [3, 4, 5]),
                items: SelectRewards(2, items, [2, 3])
            ),

            MissionFactory.Create(
                name: "Expédition florale",
                description: $"Un biome floral inconnu a été détecté sur {planets[2].Name}. " +
                             "Cette jungle luxuriante pourrait cacher autant de merveilles que de menaces.",
                coins: 2,
                planet: planets[2],
                enemies: SelectEnemies(enemies, [6, 7, 8]),
                items: SelectRewards(3, items, [4, 5])
            ),

            MissionFactory.Create(
                name: "Sonde en détresse",
                description: $"Un ancien signal d'urgence provient des étendues sauvages de {planets[3].Name}. " +
                             "Trouvez et récupérez les données à tout prix.",
                coins: 1,
                planet: planets[3],
                enemies: SelectEnemies(enemies, [9, 10, 11]),
                items: SelectRewards(4, items, [2, 4])
            ),

            MissionFactory.Create(
                name: "Nettoyage thermique",
                description: $"Une nappe de gaz explosif menace les mines de {planets[4].Name}. " +
                             "Neutralisez les risques avant une catastrophe totale.",
                coins: 3,
                planet: planets[4],
                enemies: SelectEnemies(enemies, [12, 13, 10]),
                items: SelectRewards(5, items, [7, 6])
            ),

            MissionFactory.Create(
                name: "Phénomène noir",
                description: $"Une singularité gravitationnelle est apparue à la surface de {planets[5].Name}. " +
                             "Collectez des données vitales sur ce phénomène extrême.",
                coins: 2,
                planet: planets[5],
                enemies: SelectEnemies(enemies, [1, 4, 7]),
                items: SelectRewards(6, items, [5, 3])
            ),

            MissionFactory.Create(
                name: "Terraforming partiel",
                description: $"Les opérations de terraformation sur {planets[6].Name} ont échoué. " +
                             "Restaurez les systèmes avant l'extinction des colons sur place.",
                coins: 3,
                planet: planets[6],
                enemies: SelectEnemies(enemies, [2, 5, 8]),
                items: SelectRewards(7, items, [3, 5])
            ),

            MissionFactory.Create(
                name: "Extraction de minerai",
                description: $"Des ressources précieuses ont été découvertes sur {planets[7].Name}. " +
                             "Assurez leur exploitation sous la menace constante d'ennemis.",
                coins: 2,
                planet: planets[7],
                enemies: SelectEnemies(enemies, [6, 12, 11]),
                items: SelectRewards(8, items, [1, 4, 5])
            ),
            
            MissionFactory.Create(
                name: "Les contrées du sud",
                description: $"Des rumeurs d'une ancienne technologie circulent sur {planets[0].Name}. " +
                             "Explorez les ruines et récupérez des artefacts avant que d'autres ne le fassent.",
                coins: 2,
                planet: planets[7],
                enemies: SelectEnemies(enemies, [6, 12, 11]),
                items: SelectRewards(8, items, [1, 4, 5])
            )
        };
    }

    private static IList<EnemyEntity> SelectEnemies(IList<EnemyEntity> allEnemies, int[] ids)
    {
        return allEnemies.Where(e => ids.Contains(e.Id)).ToList();
    }

    private static IList<RewardedEntity> SelectRewards(int missionId, IList<ItemEntity> allItems, int[] ids)
    {
        var random = new Random();
        return allItems
            .Where(i => ids.Contains(i.Id))
            .Select((item) => new RewardedEntity
            {
                MissionId = missionId,
                ItemId = item.Id,
                Quantity = random.Next(1, 2)
            }).ToList();
    }
}