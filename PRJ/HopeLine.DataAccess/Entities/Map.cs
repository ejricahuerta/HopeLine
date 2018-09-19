using HopeLine.DataAccess.Entities.Base;
using HopeLine.DataAccess.Repositories;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// this class will hold saved maps of what the user's last 5 saved searches were
    ///
    /// </summary>
    public class Map : CommonEntity
    {
        public Map(UnitOfWork unitOfWork)
        {

        }
        public Map()
        {
            Radius = 0;
            YCoordinate = 0;
            XCoordinate = 0;
            // LocationNames = new List<string>();
        }

        //[Required]
        //[MinLength(2)]
        //[MaxLength(500)]
        //public ICollection<string> LocationNames { get; set; }

        public double XCoordinate { get; set; }

        public double YCoordinate { get; set; }

        public double Radius { get; set; }
    }
}
