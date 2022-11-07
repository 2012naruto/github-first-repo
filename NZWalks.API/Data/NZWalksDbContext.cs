using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options): base (options)
        {

        }

        public DbSet<Region> Regions { get; set; } 
        //this is where the dbcontext will create a table named Regions for Region model class 
        //if there is nt any table,when we use code first approach

        public DbSet <Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
        
    }
}
