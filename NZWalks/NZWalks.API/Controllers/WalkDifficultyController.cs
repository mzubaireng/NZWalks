using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{

    [ApiController ]
    [Route ("WalkDifficulty")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        // inject IWalkDifficulty and IMapper

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper )
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet ]
        public async Task <IActionResult > GetAllWalkDifficultyAsync()
        {
            // get data  using walkDifficultyRepository
            var walkDifficultyDomain = await walkDifficultyRepository.GetAllAsync();

            if (walkDifficultyDomain == null) return NotFound();

            // now convert list of  domain model to DTO model
            var walkDifficultyDTO = mapper .Map <List<Models .DTO.WalkDifficulty>>(walkDifficultyDomain);

            // return response
            return Ok(walkDifficultyDTO);

        }

        [HttpGet ]
        [Route ("{id:guid}")]
        [ActionName ("GetWalkDifficulty")]
        public async Task <IActionResult > GetWalkDifficulty(Guid id)
        {
            // get data  using walkDifficultyRepository
            var walkDifficultyDomain= await walkDifficultyRepository.GetAsync (id);

            if (walkDifficultyDomain == null) return NotFound();

            // now convert  domain model to DTO model
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            // return response
            return Ok(walkDifficultyDTO);

        }

        [HttpPost ]
        public async Task <IActionResult > AddWalkDifficultyAsync([FromBody ] Models .DTO .AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            // convert DTO model to domain model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code

            };

            // now send query in db 
            // send data to database  using walkDifficultyRepository object

            walkDifficultyDomain=await  walkDifficultyRepository.AddAsync(walkDifficultyDomain);
            // its return type is WalkDifficulty

            //if (walkDifficultyDomain == null) return NotFound(); // extra
            // convert domain model to DTO for showing it to client
            var  walkDifficultyDTO = mapper.Map<Models .DTO .WalkDifficulty >(walkDifficultyDomain);

          

         return    CreatedAtAction(nameof(GetWalkDifficulty), new { Id = walkDifficultyDTO.Id }, walkDifficultyDTO);

        }

        [HttpPut]
        [Route("{id:guid}")] // restrict id for Guid

        public async Task <IActionResult > UpdateWalkDifficulty([FromRoute]Guid id, [FromBody ] Models .DTO .UpdateWalkDifficultyRequest UpdateWalkDifficultyRequest)
        {
            // convert DTO to domain model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty()
            {
                Code = UpdateWalkDifficultyRequest.Code 
            };
            // pass data to repository 
          walkDifficultyDomain=   await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);

            if (walkDifficultyDomain == null) return null;

            // convert domain model to DTO to display it to user

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return Ok(walkDifficultyDTO);


        }

        [HttpDelete ]
        [Route ("{id:guid}")]
        public async Task <IActionResult > DeleteWalkDifficulty(Guid id)
        {
            // pass id to repository to delete data and its return will be WalkDifficulty

            var WalkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDifficultyRepository == null) return NotFound();
            // convert domain model to DTO to expose it to the clinet
            var WalkDifficultyDTO = mapper.Map<Models .DTO .WalkDifficulty >(WalkDifficultyDomain);

            return Ok(WalkDifficultyDTO);

        }

    }
}
