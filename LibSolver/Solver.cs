using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math.Optimization;
using MathNet.Numerics.LinearAlgebra;

namespace LibSolver
{
    public class Solver
    {
        public double[] solve(double[] tBias, double[,] I, double[] x0, double[] b, double[][] A, double[,] limitations, double[,] extraLimitations)
        {
            int count = tBias.Length;
            double[,] W = new double[tBias.Length, tBias.Length];

            for (int i = 0; i < W.GetLength(1); i++)
                for (int j = 0; j < W.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        W[i, j] = 1 / (Math.Pow(tBias[i], 2));
                    }
                    else
                    {
                        W[i, j] = 0;
                    }
                }

            var IMatr = Matrix<double>.Build.DenseOfArray(I);
            var WMatr = Matrix<double>.Build.DenseOfArray(W);

            var HMatr = IMatr * WMatr;

            var x0Vect = Vector<double>.Build.Dense(x0);

            var dVect = HMatr * x0Vect * -1;

            var constraints = new List<LinearConstraint>();

            for (int i = 0; i < b.Length; i++)
            {
                constraints.Add(createLinearConstraint(b[i], A[i]));
            }

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (extraLimitations[i, j] > 0)
                    {
                        constraints.Add(new LinearConstraint(numberOfVariables: 2)
                        {
                            VariablesAtIndices = new int[] { i, j },
                            CombinedAs = new double[] { 1, -extraLimitations[i, j] },
                            ShouldBe = ConstraintType.EqualTo,
                            Value = 0
                        });
                    }
                }
                if (limitations[i, 0] > 0)
                {
                    constraints.Add(new LinearConstraint(numberOfVariables: 1)
                  {
                      VariablesAtIndices = new int[] { i },
                      ShouldBe = ConstraintType.GreaterThanOrEqualTo,
                      Value = limitations[i, 0]
                  });
                }
                if (limitations[i, 1] > 0)
                {
                    constraints.Add(new LinearConstraint(numberOfVariables: 1)
                    {
                        VariablesAtIndices = new int[] { i },
                        ShouldBe = ConstraintType.LesserThanOrEqualTo,
                        Value = limitations[i, 1]
                    });
                }
            }

            var solver = new GoldfarbIdnani(
                function: new QuadraticObjectiveFunction(HMatr.ToArray(), dVect.ToArray()),
                constraints: constraints);

            bool success = solver.Minimize();
            int[] activeConstraints = solver.ActiveConstraints;
            double[] solution = solver.Solution;
            return solution;
        }

        private LinearConstraint createLinearConstraint(double bElem, double[] ARow)
        {
            return new LinearConstraint(numberOfVariables: ARow.Length)
            {
                VariablesAtIndices = Enumerable.Range(0, ARow.Length).ToArray(),
                CombinedAs = ARow,
                ShouldBe = ConstraintType.EqualTo,
                Value = bElem
            };
        }
    }
}
