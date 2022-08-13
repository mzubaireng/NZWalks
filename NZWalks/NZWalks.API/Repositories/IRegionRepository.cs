using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface  IRegionRepository
    {
        // it has contract i.e. what to implement
        // we give its implementation in Region Repository class
       Task< IEnumerable<Region>> GetAllAsync();
        Task<Region> GetAsync(Guid id);
       Task<Region> AddAsync(Region region);
        Task<Region> DeleteAsync(Guid id);

        Task<Region> UpdateAsync(Guid id, Region region);


    }
}
