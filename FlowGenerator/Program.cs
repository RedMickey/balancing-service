using System;
using System.Text.Json;
using WebLab7_1.Models;

namespace FlowGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            FlowGeneratorC flowGenerator = new FlowGeneratorC(9, 8, 8);

            Flow[] flows = flowGenerator.generateNodes(5);
            string jsonBody = JsonSerializer.Serialize<Flow[]>(flows);

            Console.WriteLine(jsonBody);
        }
    }
}
