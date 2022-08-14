using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        // inject IWalkRepositroy and IMapper

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // Fetch data from database using object of IWalkRepository 
            var walksDomain = await walkRepository.GetAllAsync();



            //convert list of  domain model to List of DTO model           
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);
            // since Walk and WalkDifficulty can be assumed as same profile
            // so we can create common profile for both of them namely WalkProfile.cs
            // i.e we will map Walk domain model with DTO walk model  
            // and it will also map WalkDifficulty domain model with DTO WalkDifficulty model  
            // so create Walk.cs and WalkDifficulty.cs DTO model in DTO folder

            //if we want then we can also create different profile for WalkDifficulty

            // return response
            return Ok(walksDTO);

        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName ("GetWalkAsync")] // it will be used in add method (HttpPost] to return response
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // Fetch data from database using object of IWalkRepository 
            var walkDomain = await walkRepository.GetAsync(id);
            if (walkDomain == null)
            {
                return NotFound(); // 404
             }
            //convert domain model to DTO model      
            var walksDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
            return Ok(walksDTO);

        }

        [HttpPost]

        public async Task <IActionResult > AddWalkAsync([FromBody ]Models .DTO .AddWalkRequest addWalkRequest )
        {
            // Request (DTO) to domain model
            // convert DTO model to domain model
            var walkDomain = new Models.Domain.Walk()
            {
                Name = addWalkRequest.Name ,
                Length =addWalkRequest .Length ,
                RegionId =addWalkRequest .RegionId ,
                WalkDifficultyId =addWalkRequest .WalkDifficultyId 
                
            };

            //  pass details to repository 
            walkDomain = await  walkRepository.AddAsync(walkDomain);

            // convert walkDomain back to DTO model
            var walkDTO = mapper.Map<Models.DTO.Walk >(walkDomain );

            // return response back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }


        [HttpPut]
        [Route("{id:guid}")] // restrict id for Guid
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest  updateWalkRequest)
        {
            // convert DTO model to Domain Model

            var walkDomain = new Models.Domain.Walk ()
            {
                Name=updateWalkRequest .Name ,
                Length =updateWalkRequest .Length ,
                RegionId =updateWalkRequest .RegionId ,
                WalkDifficultyId =updateWalkRequest .WalkDifficultyId 
            };

            // update walk using repository
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            // handle null NotFound()
            if (walkDomain == null)
                return NotFound();
            // convert domain back to DTO

            var walkDTO = mapper.Map<Models.DTO.Walk >(walkDomain);
            return Ok(walkDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // get the deleted walk domain 
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null) return NotFound();

            // convert Model walk back to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk >(walkDomain);

            return Ok(walkDTO);

        }
    }
}