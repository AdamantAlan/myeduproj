using Algorithms.Frameworks;

var arr = new int[] { 8, 0, -3, 16, 5, -57, 25, 1 };

ArrayAlg.QuickSort(arr, 0, arr.Length - 1);

foreach (int i in arr)
{
    Console.WriteLine(i);
}