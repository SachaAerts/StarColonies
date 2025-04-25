using StarColonies.Infrastructures.Data.Entities.Items;
using StarColonies.Infrastructures.Data.Entities.Missions;

namespace StarColonies.Infrastructures.Data.Configurations.Seeder.Map;

public class MissionSeeder
{
    public static void SeedMissions(StarColoniesDbContext context, IList<PlanetEntity> planets,
        List<EnemyEntity> enemies, List<ItemEntity> items)
    {
        IList<MissionEntity> missions = new List<MissionEntity> {
            CreateMission("Exploration de ruines",
                $"Les restes d’une ancienne civilisation extraterrestre ont été découverts sur {planets[0].Name}. " +
                "Votre mission est de fouiller ces ruines mystérieuses pour récupérer des artefacts technologiques enfouis. " +
                "Mais attention… les lieux sont instables et infestés de créatures hostiles attirées par l’énergie résiduelle.",
                planets[0], enemies, items),

            CreateMission("Survie nocturne",
                $"Une nuit éternelle s’est abattue sur la région équatoriale de {planets[0].Name}. " +
                "Votre équipe doit survivre 48 heures dans un environnement où les prédateurs sortent de l’ombre, plus féroces que jamais. " +
                "Une épreuve de ténacité et d’endurance dans l’obscurité totale.",
                planets[0], enemies, items),

            CreateMission("Expédition florale",
                $"Un biome floral inconnu a été détecté sur {planets[1].Name}. " +
                "Cette jungle luxuriante produit des spores capables de guérir… ou de tuer. " +
                "Récupérez des échantillons, identifiez les menaces, et évitez les pièges naturels dans cet environnement aussi magnifique que mortel.",
                planets[1], enemies, items),

            CreateMission("Sonde en détresse",
                $"Un signal d’urgence provenant d’une vieille sonde coloniale a été capté sur {planets[2].Name}. " +
                "Retracez le signal, sécurisez la zone, et tentez de rapatrier ses précieuses données. " +
                "Des entités inconnues semblent toutefois protéger la zone avec une étrange hostilité.",
                planets[2], enemies, items),

            CreateMission("Nettoyage thermique",
                $"Une nappe de gaz hautement inflammable s’est échappée près des installations minières de {planets[3].Name}. " +
                "Votre objectif : neutraliser les poches de chaleur et repousser les créatures qui s’en nourrissent avant que tout ne s’embrase.",
                planets[3], enemies, items),

            CreateMission("Phénomène noir",
                $"Une anomalie gravitationnelle est apparue à la surface de {planets[4].Name}. " +
                "Le temps et l’espace y semblent distordus. Votre mission : approcher la singularité, prélever des mesures, et en ressortir vivant... si possible.",
                planets[4], enemies, items),

            CreateMission("Terraforming partiel",
                $"L’opération de terraformation de {planets[5].Name} a été interrompue par une défaillance du noyau énergétique. " +
                "Rétablissez les systèmes, sécurisez les générateurs et assurez la survie des techniciens, tout en luttant contre les formes de vie hostiles qui prolifèrent dans le chaos.",
                planets[5], enemies, items),

            CreateMission("Extraction de minerai",
                $"De rares filons de crysolithe, un minerai aux propriétés énergétiques révolutionnaires, ont été repérés sur {planets[6].Name}. " +
                "Protégez les extracteurs, surveillez les relevés géologiques, et tenez bon face aux attaques incessantes d’organismes territoriaux.",
                planets[6], enemies, items)
        };

        context.Mission.AddRange(missions);
        context.SaveChanges();
    }
    
    private static MissionEntity CreateMission(string name, string description, PlanetEntity planet, List<EnemyEntity> enemies, List<ItemEntity> items)
    {
        var selectedEnemies = enemies.OrderBy(_ => Guid.NewGuid()).Take(3).ToList();
        var difficulty = selectedEnemies.Sum(e => e.Strength);

        var mission = new MissionEntity
        {
            Name = name,
            Description = description,
            Difficulty = difficulty,
            CoinsReward = Random.Shared.Next(1, 4),
            Planet = planet,
            PlanetId = planet.Id,
            Enemies = selectedEnemies,
            Rewards = new List<RewardedEntity>()
        };

        var itemChoices = items.OrderBy(_ => Guid.NewGuid()).Take(2).ToList();

        mission.Rewards.Add(new RewardedEntity
        {
            Item = itemChoices[0],
            ItemId = itemChoices[0].Id,
            MissionId = mission.Id,
            Mission = mission,
            Quantity = 1
        });

        mission.Rewards.Add(new RewardedEntity
        {
            Item = itemChoices[1],
            ItemId = itemChoices[1].Id,
            MissionId = mission.Id,
            Mission = mission,
            Quantity = 2
        });

        return mission;
    }
}