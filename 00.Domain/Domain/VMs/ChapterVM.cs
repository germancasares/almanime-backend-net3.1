using System;

namespace Domain.VMs
{
    public class ChapterVM
    {
        public Guid ID { get; set; }

        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime? Aired { get; set; }
        public int? Duration { get; set; }
    }
}
