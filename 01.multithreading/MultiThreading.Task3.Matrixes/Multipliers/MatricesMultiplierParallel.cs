using MultiThreading.Task3.MatrixMultiplier.Matrices;
using System.Threading.Tasks;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            // todo: feel free to add your code here
            var firstMatrixRows = m1.RowCount; 
            var firstMatrixCols = m1.ColCount;
            var secondMatrixRows = m2.RowCount; 
            var secondMatrixCols = m2.ColCount;

            var result = new Matrix(firstMatrixRows, secondMatrixCols);

            Parallel.For(0, firstMatrixRows, i =>
            {
                for (int j = 0; j < secondMatrixCols; ++j)
                {
                    long res = 0;
                    for (int k = 0; k < firstMatrixCols; ++k)
                    {
                        res += m1.GetElement(i, k) * m2.GetElement(k, j);
                    }
                    result.SetElement(i, j, res);
                }
            });

            return result;
        }
    }
}
