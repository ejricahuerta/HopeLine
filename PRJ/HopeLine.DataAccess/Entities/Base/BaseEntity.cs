using System;

namespace HopeLine.DataAccess.Entities.Base
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            DateAdded = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
