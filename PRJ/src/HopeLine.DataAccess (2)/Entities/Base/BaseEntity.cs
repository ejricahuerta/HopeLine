using System;

namespace HopeLine.DataAccess.Entities.Base
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
