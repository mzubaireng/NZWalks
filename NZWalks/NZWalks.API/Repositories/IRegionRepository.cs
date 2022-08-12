using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface  IRegionRepository
    {
        // it has contract i.e. what to implement
        // we give its implementation in Region Repository class
       Task< IEnumerable<Region>> GetAllAsync();

    }
}
