using System;

namespace HopeLine.DataAccess.Entities.Base
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            DateAdded = DateTime.UtcNow.ToString();
        }
        public int Id { get; set; }
        public string DateAdded { get; set; }
    }
}
