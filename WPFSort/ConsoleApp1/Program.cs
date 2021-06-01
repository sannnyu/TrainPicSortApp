using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> count4Spilt = new List<int> { 12, 14, 12, 14, 12, 12, 12, 12, 14 };
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("当前:"+count4Spilt.GetRange(0, i).Sum());
            }
            
            
            Console.ReadKey();
         }
    }
}
