﻿namespace Domain.VMs
{
    public class PaginationMetaVM
    {
        public int Count { get; set; }
        public string First { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }
        public string Last { get; set; }
    }
}
