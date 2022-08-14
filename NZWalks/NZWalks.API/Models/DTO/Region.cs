using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class Region
    {
        public Guid Id { get; set; }
        // public string? DTOAutomapperRegionCode { get; set; } // ? is to make nullable property
        public string Code { get; set; } // ? is to make nullable property
        public string Name { get; set; }
        public double Area { get; set; }

        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }

        // navigation property
        // a region can have many walks
       // public IEnumerable<Walk> Walkes { get; set; }



    }
}
