using System;

namespace HopeLine.DataAccess.Entities.Base {
    public class BaseEntity : IEntity {
        public BaseEntity () {
            DateAdded = DateTime.Now.ToString ("MM/dd/yyyy HH:mm:ss");
        }
        public int Id { get; set; }
        public string DateAdded { get; set; }
    }
}