using HopeLine.DataAccess.Entities.Base;
using System.Collections.Generic;

namespace HopeLine.DataAccess.Entities
{
    //TODO : add props

    /// <summary>
    /// this class will hold saved maps
    /// </summary>
    public class Map : CommonEntity
    {
        public string Url { get; set; }

        public ICollection<string> LocationNames { get; set; }
    }
}
