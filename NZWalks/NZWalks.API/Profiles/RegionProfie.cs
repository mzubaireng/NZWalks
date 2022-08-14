using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionProfie :Profile
    {

        public RegionProfie()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
        }

    }
}
 