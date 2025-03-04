using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramprimeFactorization
{
    internal class ProgramprimeFactorization
    {
        static bool isPrime(int x)
        {
            for(int i=2;i*i<=x;i++)
            {
                if(x%i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            int n;
            try
            {
                n = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("请输入一个整数\n");
                return;
            }
            for(int i=2;i<=n;i++)
            {
                if (!isPrime(i)) continue;
                if (n % i == 0) Console.Write($"{i} ");

            }
            Console.WriteLine("");
        }
    }
}
