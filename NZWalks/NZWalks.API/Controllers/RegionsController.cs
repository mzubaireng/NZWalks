using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
//using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;


namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Regions")] // here we are telling that this Regions API and this will be end point
    // This Regions end point will basically map to this RegionsController
    // we can specify it with others name also example [Route("nz-regions")] 
    // or [Route("[controller]")] this basically means  [Route("Regions")] 

    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository , IMapper mapper )
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet ]
        public async Task < IActionResult> GetAllRegionsAsync()
        {
            // now comment static data
            //var regions = new List<Region>()
            //{
            //    new Region
            //    {
            //        Id =Guid .NewGuid (),
            //        Name="Wellingtonn",
            //        Code="WLG",
            //        Area =227755,
            //        Lat =-1.8822,
            //        Long=299.88,
            //        Population =500000

            //    },
            //     new Region
            //    {
            //        Id =Guid .NewGuid (),
            //        Name="Auckland",
            //        Code="AUCK",
            //        Area =227755,
            //        Lat =-1.8822,
            //        Long=299.88,
            //        Population =500000

            //    }
            //};

            var regions = await  regionRepository.GetAllAsync();
            //return DTO regions
            // create new data of region
            //var regionsDTO = new List<Models.DTO.Region>();

            //regions.ToList().ForEach(region =>
            //{
            //   var regionDTO=new Models.DTO.Region()
            //    {
            //        Id = region.Id ,
            //       RegionCode = region.Code ,
            //        Name=region.Name,
            //        Area=region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population=region .Population
            //    };
            //    regionsDTO.Add(regionDTO);

            //}   );
            var regionsDTO =mapper .Map <List<Models .DTO .Region >> (regions);
            return Ok(regionsDTO);

            // return Ok(regions); // it is basically 200 success responsen
            // it tells the client that is 200 success code and coming from Restfull api
            //https://localhost:7080/Regions
            //localhost:7080/ it is domain
            //Regions is Restful end point
        }

        [HttpGet ]
        [Route ("{id:guid}")]
        [ActionName ("GetRegionAsync")]
        public async Task <IActionResult> GetRegionAsync(Guid id)
        {

            var region = await regionRepository.GetAsync(id);// first or default can give us null
            if (region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task <IActionResult > AddRegionAsync(Models.DTO .AddRegionRequest addRegionRequest )
        {
            // Request (DTO) to domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            // pass details to repository
            region = await regionRepository.AddAsync(region);

            // coveret model back to DTO
            //var regionDTO = new Models.DTO.Region()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Area = region.Area,
            //    Lat = region.Lat,
            //    Long = region.Long,
            //    Name = region.Name,
            //    Population = region.Population
            //};

           // or
          
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);

        }
        [HttpDelete ]
        [Route ("{id:guid}")]

        public async Task <IActionResult > DeleteRegionAsync(Guid id)
        {
            // get the region from database
            var region = await regionRepository.DeleteAsync(id);

            if (region == null) return NotFound();

            // convert Model region back to DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);

        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult > UpdateRegionAsync([FromRoute]Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest  )
        {
            // convert DTO model to Domain Model

            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population

            };

            // update region using repository
            region = await regionRepository.UpdateAsync(id, region);

            if (region == null)
                return NotFound ();
            // convert domain back to DTO

            var regionDTO = mapper.Map<Models.DTO.Region>(region);


            return Ok(regionDTO);
           

        }


    }
}
