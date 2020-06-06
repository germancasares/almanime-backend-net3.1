using System.Collections.Generic;

namespace Domain.Options
{
    public class FrontendOptions
    {
        public const string Accessor = "FrontendOptions";

        public IEnumerable<string> Urls { get; set; }
    }
}
