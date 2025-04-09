using Microsoft.EntityFrameworkCore;

namespace StarColonies.Infrastructures.Data;

public class StarColoniesContext : DbContext
{
    public StarColoniesContext(DbContextOptions options) : base(options)
    {
        
    }
}