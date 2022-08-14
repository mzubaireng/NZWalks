using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface  IWalkRepository    {
        // Define contracts

        public  Task<IEnumerable<Walk>> GetAllAsync();
        public Task<Walk> GetAsync(Guid id);
        public Task<Walk> AddAsync(Walk walk );

         public Task<Walk> UpdateAsync(Guid id, Walk walk);
         public Task<Walk> DeleteAsync(Guid id);




    }
}
