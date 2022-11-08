using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();  // this is to get all methods

        Task<Region> GetAsync(Guid id);    //this is to get region based on id provided


        Task<Region> AddnewAsync(Region region);  //insert operation
         Task<Region> DeleteAsync(Guid id);      //delete

        Task<Region> UpdateAsync(Guid id,Region region);   //update
    }



}
