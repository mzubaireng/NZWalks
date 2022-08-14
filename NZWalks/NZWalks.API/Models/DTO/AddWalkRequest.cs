namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequest
    {
        // this will be used to update operation
      
        public string Name { get; set; }
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

    }
}
