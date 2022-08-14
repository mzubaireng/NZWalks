namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequest
    {
        // we can remove any properyty here if we do not want to modify it
        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }


    }
}
