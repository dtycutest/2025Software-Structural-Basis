using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayCul
{
    internal class Program
    {
        static int getMax(int[] a)
        {
            int res=a[0];
            for(int i=1;i<a.Length;i++) res = Math.Max(res, a[i]);
            return res;
        }
        static int getMin(int[] a)
        {
            int res = a[0];
            for (int i = 1; i < a.Length; i++) res = Math.Min(res, a[i]);
            return res;
        }
        static int getSum(int[] a)
        {
            int res =0;
            for (int i = 0; i < a.Length; i++) res += a[i];
            return res;
        }
        static double getAvg(int[] a)
        {
            double res = 1.0*getSum(a)/a.Length;
            return res;
        }
        static void Main(string[] args)
        {
            char ch = ' ';
            string[] arr = Console.ReadLine().Split(ch);
            int[] a = new int[arr.Length];
            try
            {
                a = Array.ConvertAll<string, int>(arr, m => int.Parse(m));
            }
            catch
            {
                Console.WriteLine("请以空格分隔输入一行整数");
                return;
            }
            int aMax = getMax(a);
            int aMin= getMin(a);
            double aAvg = getAvg(a);
            int aSum=getSum(a);
            Console.WriteLine($"数组最大值为{aMax}");
            Console.WriteLine($"数组最小值为{aMin}");
            Console.WriteLine($"数组平均值为"+aAvg.ToString("0.00"));
            Console.WriteLine($"数组的总和为{aSum}");
        }
    }
}
