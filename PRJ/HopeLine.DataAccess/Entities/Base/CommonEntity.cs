using System;

namespace HopeLine.DataAccess.Entities.Base
{
    public class CommonEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
