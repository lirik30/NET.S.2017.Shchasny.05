using System;
using static Double.Extensions.DoubleExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            double d = 255.255;
            Console.WriteLine(d.ToBinary());
        }
    }
}
