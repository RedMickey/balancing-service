using FlowGenerator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebLab7_1;
using WebLab7_1.Models;

namespace NUnitTestProject2
{
    public class Tests
    {
        private TestServer _server;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
               .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public async Task Test1()
        {
            HttpContent req = new StringContent(
                @"
                    {
                        ""Flows"": [
                            {
                            ""Id"": 1,
                            ""X0"": 10.005,
                            ""Tolerance"": 0.200,
                            ""SourceId"": -1,
                            ""DestId"": 1
                            },
                            {
                            ""Id"": 2,
                            ""X0"": 3.033,
                            ""Tolerance"": 0.121,
		                    ""SourceId"": 1,
                            ""DestId"": -1
                            },
                            {
                            ""Id"": 3,
                            ""X0"": 6.831,
                            ""Tolerance"": 0.683,
                            ""SourceId"": 1,
                            ""DestId"": 2
                            },
                            {
                            ""Id"": 4,
                            ""X0"": 1.985,
                            ""Tolerance"": 0.040,
                            ""SourceId"": 2,
                            ""DestId"": -1
                            },
                            {
                            ""Id"": 5,
                            ""X0"": 5.093,
                            ""Tolerance"": 0.102,
                            ""SourceId"": 2,
                            ""DestId"": 3
                            },
                            {
                            ""Id"": 6,
                            ""X0"": 4.057,
                            ""Tolerance"": 0.081,
                            ""SourceId"": 3,
                            ""DestId"": -1
                            },
                            {
                            ""Id"": 7,
                            ""X0"": 0.991,
                            ""Tolerance"": 0.020,
                            ""SourceId"": 3,
                            ""DestId"": -1
                            }
                        ],
                        ""count"": 7
                    }
                ",
                Encoding.UTF8,
                "application/json"
            );
            // Act
            var response = await _client.PostAsync("/api/balance/calculate", req);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            var res = JsonSerializer.Deserialize<BalanceResponse>(responseString);
            // Assert
            Assert.IsTrue(res.BalanceResolved);
        }

        [Test]
        public async Task Test2()
        {
            HttpContent req = new StringContent(
                @"
                  {
                    ""Flows"": [
                      {
                        ""Id"": 1,
                        ""X0"": 10.005492,
                        ""Tolerance"": 0.200,
                        ""SourceId"": -1,
                        ""DestId"": 1
                      },
                      {
                        ""Id"": 2,
                        ""X0"": 3.032658,
                        ""Tolerance"": 0.121,
                        ""SourceId"": 1,
                        ""DestId"": -1
                      },
                      {
                        ""Id"": 3,
                        ""X0"": 6.831220,
                        ""Tolerance"": 0.683,
                        ""SourceId"": 1,
                        ""DestId"": 2
                      },
                      {
                        ""Id"": 4,
                        ""X0"": 1.984785,
                        ""Tolerance"": 0.040,
                        ""SourceId"": 2,
                        ""DestId"": -1
                      },
                      {
                        ""Id"": 5,
                        ""X0"": 5.092934,
                        ""Tolerance"": 0.102,
                        ""SourceId"": 2,
                        ""DestId"": 3
                      },
                      {
                        ""Id"": 6,
                        ""X0"": 4.057213,
                        ""Tolerance"": 0.081,
                        ""SourceId"": 3,
                        ""DestId"": -1
                      },
                      {
                        ""Id"": 7,
                        ""X0"": 0.991215,
                        ""Tolerance"": 0.020,
                        ""SourceId"": 3,
                        ""DestId"": -1
                      },
                      {
                        ""Id"": 8,
                        ""X0"": 6.666660,
                        ""Tolerance"": 0.667,
                        ""SourceId"": -1,
                        ""DestId"": 3
                      }
                    ],
                    ""count"": 8
                  }
                ",
                Encoding.UTF8,
                "application/json"
            );
            // Act
            var response = await _client.PostAsync("/api/balance/calculate", req);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            var res = JsonSerializer.Deserialize<BalanceResponse>(responseString);
            // Assert
            Assert.IsTrue(res.BalanceResolved);
        }

        [Test]
        public async Task Test3()
        {
            HttpContent req = new StringContent(
                @"
                  {
                    ""Flows"": [
                      {
                        ""Id"": 1,
                        ""X0"": 10.005492,
                        ""Tolerance"": 0.200,
                        ""Limitations"": [
                          {
                            ""FlowId"": 2,
                            ""Coefficient"": 10
                          }
                        ],
                        ""SourceId"": -1,
                        ""DestId"": 1
                      },
                      {
                        ""Id"": 2,
                        ""X0"": 3.032658,
                        ""Tolerance"": 0.121,
                        ""SourceId"": 1,
                        ""DestId"": -1
                      },
                      {
                        ""Id"": 3,
                        ""X0"": 6.831220,
                        ""Tolerance"": 0.683,
                        ""SourceId"": 1,
                        ""DestId"": 2
                      },
                      {
                        ""Id"": 4,
                        ""X0"": 1.984785,
                        ""Tolerance"": 0.040,
                        ""SourceId"": 2,
                        ""DestId"": -1
                      },
                      {
                        ""Id"": 5,
                        ""X0"": 5.092934,
                        ""Tolerance"": 0.102,
                        ""SourceId"": 2,
                        ""DestId"": 3
                      },
                      {
                        ""Id"": 6,
                        ""X0"": 4.057213,
                        ""Tolerance"": 0.081,
                        ""SourceId"": 3,
                        ""DestId"": -1
                      },
                      {
                        ""Id"": 7,
                        ""X0"": 0.991215,
                        ""Tolerance"": 0.020,
                        ""SourceId"": 3,
                        ""DestId"": -1
                      },
                      {
                        ""Id"": 8,
                        ""X0"": 6.666660,
                        ""Tolerance"": 0.667,
                        ""SourceId"": -1,
                        ""DestId"": 3
                      }
                    ],
                    ""count"": 8
                  }
                ",
                Encoding.UTF8,
                "application/json"
            );
            // Act
            var response = await _client.PostAsync("/api/balance/calculate", req);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            var res = JsonSerializer.Deserialize<BalanceResponse>(responseString);

            // Assert
            Assert.IsTrue(res.BalanceResolved);
        }

        [Test, Timeout(10000)]
        public async Task Test4()
        {
            FlowGeneratorC flowGenerator = new FlowGeneratorC();
            int nodeCount = 100;
            Flow[] flows = flowGenerator.generateNodes(nodeCount);

            Console.WriteLine("Количество узлов " + nodeCount);
            Console.WriteLine("Количество потоков " + flows.Length);

            RequestBody requestBody = new RequestBody() { Flows = flows, count = flows.Length };

            string jsonBody = JsonSerializer.Serialize<RequestBody>(requestBody);

            HttpContent req = new StringContent(
                jsonBody,
                Encoding.UTF8,
                "application/json"
            );
            // Act
            var response = await _client.PostAsync("/api/balance/calculate", req);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            var res = JsonSerializer.Deserialize<BalanceResponse>(responseString);

            // Assert
            Assert.IsTrue(res.BalanceResolved);
        }

        [Test, Timeout(200000)]
        public async Task Test5()
        {
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();

            FlowGeneratorC flowGenerator = new FlowGeneratorC(9, 9, 9);
            int nodeCount = 800;
            Flow[] flows = flowGenerator.generateNodes(nodeCount);

            Console.WriteLine("Количество узлов " + nodeCount);
            Console.WriteLine("Количество потоков " + flows.Length);

            RequestBody requestBody = new RequestBody() { Flows = flows, count = flows.Length };

            string jsonBody = JsonSerializer.Serialize<RequestBody>(requestBody);

            stopwatch1.Stop();

            Console.WriteLine("Потрачено тактов на генерацию: " + (stopwatch1.ElapsedMilliseconds));

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();

            HttpContent req = new StringContent(
                jsonBody,
                Encoding.UTF8,
                "application/json"
            );
            // Act
            var response = await _client.PostAsync("/api/balance/calculate", req);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            stopwatch2.Stop();

            Console.WriteLine("Потрачено тактов на рассчет: " + (stopwatch2.ElapsedMilliseconds));

            var res = JsonSerializer.Deserialize<BalanceResponse>(responseString);

            // Assert
            Assert.IsTrue(res.BalanceResolved);
        }

        [Test, Timeout(20000)]
        public async Task Test6()
        {
            FlowGeneratorC flowGenerator = new FlowGeneratorC(9, 6, 3);
            int nodeCount = 200;
            Flow[] flows = flowGenerator.generateNodes(nodeCount);

            Console.WriteLine("Количество узлов " + nodeCount);
            Console.WriteLine("Количество потоков " + flows.Length);

            RequestBody requestBody = new RequestBody() { Flows = flows, count = flows.Length };

            string jsonBody = JsonSerializer.Serialize<RequestBody>(requestBody);

            HttpContent req = new StringContent(
                jsonBody,
                Encoding.UTF8,
                "application/json"
            );
            // Act
            var response = await _client.PostAsync("/api/balance/calculate", req);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();

            var res = JsonSerializer.Deserialize<BalanceResponse>(responseString);

            // Assert
            Assert.IsTrue(res.BalanceResolved);
        }
    }
}