using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeFactorialization
{
    class Program
    {
        static HashSet<int> potentialPrimes;
        static HashSet<int> actualPrimes;
        static HashSet<int> primeFilters = new HashSet<int>();
        static HashSet<int> nonPrimes = new HashSet<int>();
        static Random rand = new Random();
        static int goal;
        static int numRuns = 1000;        
        static int rangeLow = 3;
        static int rangeHigh = 1000000;
        static bool logging = true;

        static void Main(string[] args)
        {
            for (int runs = 1; runs <= numRuns; runs++)
            {
                Run(runs);
            }

            Write("\nDone!");
            if (logging)
            {                
                Console.ReadKey();
            }
        }

        static void Write(string line)
        {
            if (logging)
                Console.WriteLine(line);
        }


        static void Run(int i)
        {
            potentialPrimes = new HashSet<int>();
            actualPrimes = new HashSet<int>();

            goal = rand.Next(rangeLow, rangeHigh);
            Write(string.Format("Run #{0:n0}. Prime Factors of {1:n0}: ", i, goal));
            Factorialize(goal);  
            CalculatePrimes();            
            Write(string.Format("{0}\n", string.Join(", ", Sort())));
        }

        static int[] Sort()
        {
            int[] returnArray = actualPrimes.ToArray<int>();
            Array.Sort(returnArray);
            return returnArray;
        }
       

        static void CalculatePrimes()
        {
            primeFilters.Add(2);

            bool isPrime;
            foreach (int x in potentialPrimes)
            {
                isPrime = true;
                if (nonPrimes.Contains(x))
                    isPrime = false;
                else if (!primeFilters.Contains(x))
                {
                    for (int i = 2; i <= Math.Ceiling(Math.Sqrt(x)); i++)
                    {
                        isPrime = true;
                        if (x % i == 0)
                        {
                            isPrime = false;
                            nonPrimes.Add(x);
                            break;
                        }
                    }
                }

                if (isPrime)
                {
                    primeFilters.Add(x);
                    actualPrimes.Add(x);
                }
            }
        } 

        static void Factorialize(int num)
        {
            if (actualPrimes.Contains(num) || potentialPrimes.Contains(num) || nonPrimes.Contains(num))
                return;

            potentialPrimes.Add(num);

            for (int i = 2; i < num; ++i)
            {
                if (num % i == 0)
                {
                    potentialPrimes.Add(i);
                    Factorialize(i);
                    Factorialize(num / i);
                }
            }
        }


    }
}
