namespace StarColonies.Infrastructures.Data.Seeder;

public interface IDataBaseSeeder
{
    void Seed(StarColoniesDbContext context);
}