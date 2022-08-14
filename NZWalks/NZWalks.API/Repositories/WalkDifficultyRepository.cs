using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories{
    public class WalkDifficultyRepository: IWalkDifficultyRepository 

    {
        private readonly NZWalksDbContext nZWalksDbContext;

        // inject database
        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext )
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task <IEnumerable <WalkDifficulty>> GetAllAsync()
        {
            // find data the from database 
        return await nZWalksDbContext.WalkDifficulty.ToListAsync();
        }
        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulty.FindAsync (id);
        }
        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            //override id
            walkDifficulty.Id = Guid.NewGuid();
           await  nZWalksDbContext .WalkDifficulty.AddAsync (walkDifficulty);
           await  nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;


        }
        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            // find the existing record
          var existingWalkDifficulty=  await nZWalksDbContext .WalkDifficulty .FindAsync (id);
            if (existingWalkDifficulty ==null) return null;

            existingWalkDifficulty.Code = walkDifficulty.Code;

          await  nZWalksDbContext.SaveChangesAsync();

            return existingWalkDifficulty;

        }
        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            // find the existing record
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
            if (existingWalkDifficulty == null) return null;

            nZWalksDbContext.WalkDifficulty.Remove(existingWalkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
