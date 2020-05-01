using System;

namespace Domain.Models
{
    public abstract class BaseModel
    {
        public Guid ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
