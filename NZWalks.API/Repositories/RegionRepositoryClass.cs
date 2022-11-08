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

        public async Task<Region> AddnewAsync(Region region)
        {
            region.Id= Guid.NewGuid();
           
                await nZWalksDbContext.AddAsync(region);
                await nZWalksDbContext.SaveChangesAsync();
            
                return region;
            
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region= await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
             
            //delete the region
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task <IEnumerable<Region>> GetAllAsync()
        {
           return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingregion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existingregion == null)
            {
                return null;
            }
            existingregion.Code = region.Code;
            existingregion.Name = region.Name;
            existingregion.Area= region.Area;
            existingregion.Lat=region.Lat;
            existingregion.Long = region.Long;
            existingregion.Population=region.Population;

            await nZWalksDbContext.SaveChangesAsync();

            return existingregion;
        }
    }
}
