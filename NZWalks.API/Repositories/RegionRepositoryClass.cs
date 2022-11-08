using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepositoryClass : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepositoryClass(NZWalksDbContext nZWalksDbContext) //using constructor we connection with dbcontext
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task <IEnumerable<Region>> GetAllAsync()
        {
           return await nZWalksDbContext.Regions.ToListAsync();
        }
    }
}
