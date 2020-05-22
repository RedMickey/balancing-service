using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebLab7_1.Models
{
    public class ExtraConstraint
    {
        [JsonPropertyName("flowId")]
        public int FlowId { get; set; }

        [JsonPropertyName("coefficient")]
        public double Coefficient { get; set; }
    }
}
