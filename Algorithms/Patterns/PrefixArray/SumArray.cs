namespace Algorithms.Patterns.PrefixArray
{
    /// <summary>
    /// Массив сумм O(n*m)
    /// задачи на максимизации суммы по матрице
    /// агрегаты в многомерном массиве
    /// </summary>
    internal class SumArray
    {
        /// <summary>
        /// Найти большую сумму в матрице если считать от текущей ячейки крест
        /// </summary>
        public int GetBestCrossSumFromMatrix(int[][] matrix)
        {
            var rows = matrix.Length;
            var cols = matrix[0].Length;

            var rowSums = new int[rows];
            var colSums = new int[cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    var current = matrix[i][j];

                    rowSums[i] += current;
                    colSums[j] += current;
                }

            var maxSum = 0;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    var currentSum = rowSums[i] + colSums[j] - matrix[i][j];

                    if(i is 0 && j is 0)
                    {
                        maxSum = currentSum;
                    }

                    maxSum = Math.Max(maxSum, currentSum);
                }

            return maxSum;
        }
    }
}
