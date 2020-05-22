using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibSolver;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLab7_1.Models;
using WebLab7_1.Utils;

namespace WebLab7_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        [HttpPost]
        [Route("calculate")]
        public BalanceResponse CalculateBalance(RequestBody requestBody)
        {
            double[] tBias = new double[requestBody.count];
            double[] x0 = new double[requestBody.count];
            double[,] extraLimitations = new double[requestBody.count, requestBody.count];
            int max = -1;

            for (int i = 0; i < requestBody.count; i++)
            {
                tBias[i] = requestBody.Flows[i].Tolerance;
                x0[i] = requestBody.Flows[i].X0;
                max = Math.Max(requestBody.Flows[i].DestId, max);
                max = Math.Max(requestBody.Flows[i].SourceId, max);
            }

            double[] b = Enumerable.Repeat<double>(0, max).ToArray();
            double[][] A = new double[max][];
            for (int i = 0; i < max; i++)
            {
                A[i] = new double[requestBody.count];
            }

            double[,] limitations = new double[requestBody.count, 2];

            for (int i = 0; i < requestBody.count; i++)
            {
                for (int j = 0; j < requestBody.count; j++)
                {
                    extraLimitations[i, j] = -1;
                }
            }

            for (int i = 0; i < requestBody.count; i++)
            {
                for (int j = 0; j < max; j++)
                {
                    if (requestBody.Flows[i].DestId == j + 1)
                        A[j][i] = 1;
                    else
                    {
                        if (requestBody.Flows[i].SourceId == j + 1)
                            A[j][i] = -1;
                        else
                            A[j][i] = 0;
                    }
                }
            }
            for (int i = 0; i < requestBody.count; i++)
            {
                limitations[i, 0] = requestBody.Flows[i].LowerBound;
                limitations[i, 1] = requestBody.Flows[i].UpperBound;

                if (requestBody.Flows[i].Limitations != null && requestBody.Flows[i].Limitations.Count != 0)
                {
                    for (int j = 0; j < requestBody.Flows[i].Limitations.Count; j++)
                    {
                        extraLimitations[i, requestBody.Flows[i].Limitations[j].FlowId - 1] = requestBody.Flows[i].Limitations[j].Coefficient;
                    }
                }
            }

            double[,] I = new double[requestBody.count, requestBody.count];
            for (int i = 0; i < requestBody.count; i++)
            {
                for (int j = 0; j < requestBody.count; j++)
                {
                    if (i == j)
                    {
                        I[i, j] = 1;
                    } else
                    {
                        I[i, j] = 0;
                    }
                }
            }

            Solver solver = new Solver();
            double[] res = solver.solve(tBias, I, x0, b, A, limitations, extraLimitations);

            double[] balance = BalanceUtils.calculateBalance(res, A);

            bool balanceResolved = BalanceUtils.isBalanceResolved(balance);

            return new BalanceResponse() { X = res, BalanceResolved = balanceResolved, Balance = balance };
        }
    }
}