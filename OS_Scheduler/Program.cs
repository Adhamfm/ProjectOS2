using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Scheduler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process P1 = new Process("Process 1", 10, 2);
            Process P2 = new Process("Process 2", 12, 5);
            Process P3 = new Process("Process 3", 4, 6);


            Console.ReadLine();
            
        }
    }
}
