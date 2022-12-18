using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    public class DataContex: DbContext
    {
        public DataContex(DbContextOptions<DataContex> options) : base(options) { }
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
