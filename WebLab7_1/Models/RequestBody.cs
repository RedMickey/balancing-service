using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebLab7_1.Models
{
    public class RequestBody
    {
        [JsonPropertyName("flows")]
        public Flow[] Flows { get; set; }

        [JsonPropertyName("count")]
        public int count { get; set; }
    }
}
