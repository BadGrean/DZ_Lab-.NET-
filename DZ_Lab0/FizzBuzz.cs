using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class FizzBuzz
    {
        private int limiter;
        public FizzBuzz(int limit) 
        {
            limiter = limit;
        }
        public void Display()
        {
            bool fizz;
            bool buzz;

            for (int i = 1; i < limiter + 1; i++) 
            {
                fizz = i % 3 == 0;
                buzz = i % 5 == 0;

                if (!fizz && !buzz)
                {
                    Console.WriteLine(i);
                    continue;
                }
                if (fizz)
                    Console.Write("Fizz");
                if (buzz)
                    Console.Write("Buzz");
                Console.Write("\n");

            }
        }
    }
}
