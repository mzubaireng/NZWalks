using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository 
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }


        public async  Task<IEnumerable<Walk>> GetAllAsync()
        { // it will give null for navigational properties
          //  return await  nZWalksDbContext.Walks.ToListAsync (); 

            // to get values of navigational properties use Include keyword

            var walks= await
               nZWalksDbContext.Walks
               .Include(x => x.Region)
               .Include(x => x.WalkDifficulty)
               .ToListAsync();

            /*  circular reference problem
             
            if we enable following navigation property 

            public IEnumerable<Walk> Walkes { get; set; }

            in Region domain model class (Region.cs) then we get circular reference problem
            // So we have applied comment to this property in Region.cs domain model class.

          // navigation property
        // a region can have many walks
       // public IEnumerable<Walk> Walkes { get; set; }

            // Handle circular reference problem with following code  if it exists
            //var JSonResult = JsonConvert.SerializeObject(
            //    walks ,Formatting.None,
            //    new JsonSerializerSettings ()

            //    {
            //        ReferenceLoopHandling =ReferenceLoopHandling.Ignore 
            //    }
            //    );

       //  var walksJsonDeserialisze=   JsonConvert.DeserializeObject<IEnumerable<Walk>>(JSonResult);
           
            */
            return walks;
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            // return await nZWalksDbContext.Walks.FirstOrDefaultAsync(x=>x.Id==id);



            return await nZWalksDbContext.Walks
               .Include(x => x.Region)
               .Include(x => x.WalkDifficulty)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

    public async  Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid(); // override id
            await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;

        }

      public async  Task<Walk> UpdateAsync(Guid id, Walk walk )

        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);// for primary key
            //var existingWalk = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null) return null;

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;

            await nZWalksDbContext.SaveChangesAsync();
            return existingWalk;

        }

       public async  Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await nZWalksDbContext.Walks.FindAsync(id);// for primary key
            if (existingWalk == null) return null;
            nZWalksDbContext.Walks.Remove(existingWalk);
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalk;
        }



      

    }
        

    }

