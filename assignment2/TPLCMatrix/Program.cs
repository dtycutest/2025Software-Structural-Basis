using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPLCMatrix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n, m;
            Console.WriteLine("请输入矩阵行数：");
            n=int.Parse(Console.ReadLine());
            Console.WriteLine("请输入矩阵列数：");
            m = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入矩阵：");
            int[][] a = new int[n][];
            for(int i=0;i<n;i++)
            {
                char ch = ' ';
                string[] arr = Console.ReadLine().Split(ch);
                a[i] = new int[m];
                for(int j=0;j<m;j++)
                {
                    a[i][j] = int.Parse(arr[j]);
                }
            }
            bool flag = true;
            for(int i = 0; i < m; i++)
            {
                for(int j = 1; j < n; j++)
                {
                    if (j >= n || i + j >= m) break;
                    if (a[j][i + j] != a[0][i]) flag = false;
                }
            }
            for(int i=0; i<n ; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if (i + j >= n || j >= m) break;
                    if (a[i + j][j] != a[i][0]) flag = false;
                }
            }
            Console.WriteLine(flag);
        }
    }
}
