using Domain.Enums;
using System;

namespace Domain.VMs
{
    public class SubtitleVM
    {
        public ESubtitleStatus Status { get; set; }
        public string Format { get; set; }
        public string Url { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
