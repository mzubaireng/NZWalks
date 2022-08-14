using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class WalkProfile:Profile
    {
        public WalkProfile()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>().ReverseMap();
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>().ReverseMap();
            // since Walk and WalkDifficulty belongs to same profile i.e. walk so We have 
            // added both of them here not in seperate profile.
        }

    }
}
