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
                name: "Ruins Exploration",
                description:
                $"The remains of an ancient alien civilization have been discovered on {planets[0].Name}. " +
                "Your mission is to explore these mysterious ruins and recover buried technological artifacts.",
                coins: 10,
                planet: planets[0],
                enemies: SelectEnemies(enemies, [0, 1, 2]),
                items: SelectRewards(items, [5, 1])
            ),

            MissionFactory.Create(
                name: "Night Survival",
                description: $"An eternal night has fallen over {planets[1].Name}. " +
                             "Your team must survive 48 hours in a hostile and dark environment.",
                coins: 3,
                planet: planets[1],
                enemies: SelectEnemies(enemies, [3, 4, 5]),
                items: SelectRewards(items, [2, 3])
            ),

            MissionFactory.Create(
                name: "Floral Expedition",
                description: $"An unknown floral biome has been detected on {planets[2].Name}. " +
                             "This lush jungle could hide as many wonders as threats.",
                coins: 2,
                planet: planets[2],
                enemies: SelectEnemies(enemies, [6, 7, 8]),
                items: SelectRewards(items, [4, 5])
            ),

            MissionFactory.Create(
                name: "Probe in Distress",
                description: $"An ancient emergency signal is coming from the wild regions of {planets[3].Name}. " +
                             "Find and recover the data at all costs.",
                coins: 1,
                planet: planets[3],
                enemies: SelectEnemies(enemies, [9, 10, 11]),
                items: SelectRewards(items, [2, 4])
            ),

            MissionFactory.Create(
                name: "Thermal Cleanup",
                description: $"An explosive gas cloud threatens the mines of {planets[4].Name}. " +
                             "Neutralize the risks before a total catastrophe strikes.",
                coins: 3,
                planet: planets[4],
                enemies: SelectEnemies(enemies, [12, 13, 10]),
                items: SelectRewards(items, [7, 6])
            ),

            MissionFactory.Create(
                name: "Black Phenomenon",
                description: $"A gravitational singularity has appeared on the surface of {planets[5].Name}. " +
                             "Collect vital data on this extreme phenomenon.",
                coins: 2,
                planet: planets[5],
                enemies: SelectEnemies(enemies, [1, 4, 7]),
                items: SelectRewards(items, [5, 3])
            ),

            MissionFactory.Create(
                name: "Partial Terraforming",
                description: $"Terraforming operations on {planets[6].Name} have failed. " +
                             "Restore the systems before the extinction of the settlers on site.",
                coins: 3,
                planet: planets[6],
                enemies: SelectEnemies(enemies, [2, 5, 8]),
                items: SelectRewards(items, [3, 5])
            ),

            MissionFactory.Create(
                name: "Ore Extraction",
                description: $"Precious resources have been discovered on {planets[7].Name}. " +
                             "Ensure their extraction under the constant threat of enemies.",
                coins: 120,
                planet: planets[7],
                enemies: [SelectEnemyByName(enemies, "Leviathan"), SelectEnemyByName(enemies, "Hégémon"), SelectEnemyByName(enemies, "Drone de combat")],
                items: [SelectRewardByName(items, "Golden Apple"), SelectRewardByName(items, "Uncommon Artifact")]
            ),
            
            MissionFactory.Create(
                name: "The Forbidden Souk",
                description: $"In a forgotten corner, a mysterious bazaar appeared overnight. " +
                             $"Two legendary figures inhabit it: the cunning Emir Ayoub and the fearsome Sultan Ilhan. " +
                             $"Half-merchants, half-warlords, they offer 'golden deals' to daring travelers... provided they survive their infernal challenges.\n" +
                             $"Between cosmic kebabs and anti-gravity flying carpets, settlers risk (or grow) their fortunes through risky negotiations",
                coins: 230,
                planet: planets[7],
                enemies: [SelectEnemyByName(enemies, "Ayoub"), SelectEnemyByName(enemies, "Ilhan")],
                items: [SelectRewardByName(items, "Golden Kebab"), SelectRewardByName(items, "AK-47")]
            )
        };
        
    }

    private static IList<EnemyEntity> SelectEnemies(IList<EnemyEntity> allEnemies, int[] ids)
        => allEnemies.Where(e => ids.Contains(e.Id)).ToList();

    private static IList<RewardedEntity> SelectRewards(IList<ItemEntity> allItems, int[] ids)
    {
        var random = new Random();
        return allItems
            .Where(i => ids.Contains(i.Id))
            .Select((item) => new RewardedEntity
            {
                ItemId = item.Id,
                Quantity = random.Next(1, 2)
            }).ToList();
    }
    
    private static RewardedEntity SelectRewardByName(IList<ItemEntity> allItems, string name)
    {
        var item = allItems.FirstOrDefault(i => i.Name == name);
        return new RewardedEntity
        {
            ItemId = item.Id,
            Quantity = 1
        };
    }
    
    private static EnemyEntity SelectEnemyByName(IList<EnemyEntity> allEnemies, string name)
        => allEnemies.First(e => e.Name == name);
}