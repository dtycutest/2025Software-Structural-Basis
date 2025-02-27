using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulTwo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("输入第一个数字：");
            int a=int.Parse(Console.ReadLine());
            Console.WriteLine("输入第二个数字：");
            int b= int.Parse(Console.ReadLine());
            Console.WriteLine("输入运算符：");
            int opt = Console.Read();
            int ans=0;
            if (opt == '+') ans = a + b;
            if (opt == '-') ans = a - b;
            if (opt == '*') ans = a * b;
            if (opt == '/')
            {
                if(b!=0)
                    ans = a / b;
                else
                {
                    Console.WriteLine("不可以除以零TuT");
                    return;
                }
            }
            if (opt == '%') ans = a % b;
            Console.WriteLine(ans);
        }
    }
}
