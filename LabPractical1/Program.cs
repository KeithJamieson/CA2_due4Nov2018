using System;
using System.Linq;
using System.Threading;
using System.Collections.Concurrent;

namespace LabPractical1
{
    class Program
    {
        static void Main(string[] args)
        {
            int myNumber = 0;
            Console.WriteLine("Enter a number ");

            myNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"The number entered was {myNumber}");
            Console.ReadKey();
        }
    }
}
