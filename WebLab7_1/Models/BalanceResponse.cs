using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebLab7_1.Models
{
    public class BalanceResponse
    {
        [JsonPropertyName("x")]
        public double[] X { get; set; }
        [JsonPropertyName("balance")]
        public double[] Balance { get; set; }
        [JsonPropertyName("balanceResolved")]
        public bool BalanceResolved { get; set; }
    }
}
