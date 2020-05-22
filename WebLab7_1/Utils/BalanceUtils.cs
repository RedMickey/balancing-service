using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLab7_1.Utils
{
    public class BalanceUtils
    {
        public static double[] calculateBalance(double[] X, double[][] A)
        {
            double[,] a = new double[A.Length, A[0].Length];

            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < A[0].Length; j++)
                {
                    a[i, j] = A[i][j];
                }

            var AMatr = Matrix<double>.Build.DenseOfArray(a);
            var xVect = Vector<double>.Build.Dense(X);

            var resVect = AMatr * xVect;

            return resVect.AsArray();
        }

        public static bool isBalanceResolved(double[] balance, double accuracy = 0.0001 )
        {
            foreach(double res in balance)
            {
                if (res > accuracy)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
