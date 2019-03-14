using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BaseModel
    {
        public BaseModel(bool initialize = true)
        {
            if (!initialize) return;

            ID = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            ModificationDate = DateTime.UtcNow;
        }

        [Key]
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
