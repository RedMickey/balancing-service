using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebLab7_1.Models
{
    public class Flow
    {
        [JsonPropertyName("id")]
        public int Id { set; get; }

        [JsonPropertyName("x0")]
        public double X0 { set; get; }

        [JsonPropertyName("tolerance")]
        public double Tolerance { set; get; }

        [JsonPropertyName("sourceId")]
        public int SourceId { set; get; }

        [JsonPropertyName("destId")]
        public int DestId { set; get; }

        [JsonPropertyName("lowerBound")]
        public Double LowerBound { set; get; }

        [JsonPropertyName("upperBound")]
        public Double UpperBound { set; get; }

        [JsonPropertyName("limitations")]
        public List<ExtraConstraint> Limitations { set; get; }

    }
}
