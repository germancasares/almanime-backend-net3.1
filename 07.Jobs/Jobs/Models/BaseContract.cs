using System;
using System.Text.Json;

namespace Jobs.Models
{
    public abstract class BaseContract
    {
        public override string ToString() => Convert.ToBase64String(JsonSerializer.SerializeToUtf8Bytes(this));
    }
}
