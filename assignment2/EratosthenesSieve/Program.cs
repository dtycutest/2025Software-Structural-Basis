using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EratosthenesSieve
{
    internal class Program
    {
        static void Sieve(int[] a, int range, ref int t)
        {
            bool[] v = new bool[range + 1];
            for (int i = 2; i <= range; i++)
            {
                if (v[i]) continue;
                a[t++] = i;
                for (int j = i + i; j <= range; j+=i)
                {
                    v[j] = true;
                }
            }
        }
        static void Main(string[] args)
        {
            int range = 100;
            int[] ans=new int[range+1];
            int l=0;
            Sieve(ans,range,ref l);
            for (int i = 0; i < l; i++) Console.Write($"{ans[i]} ");
            Console.WriteLine();
        }
    }
}
