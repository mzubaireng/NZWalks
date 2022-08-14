using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {

        public Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        public Task<WalkDifficulty> GetAsync(Guid id);
        public Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty);
        public Task<WalkDifficulty> UpdateAsync(Guid id,WalkDifficulty walkDifficulty);
        public Task<WalkDifficulty> DeleteAsync(Guid id);


    }
}
