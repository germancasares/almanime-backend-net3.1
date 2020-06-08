using Newtonsoft.Json;
using System;
using System.Text;

namespace Jobs.Models
{
    public abstract class BaseContract
    {
        public override string ToString() => Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));
    }
}
