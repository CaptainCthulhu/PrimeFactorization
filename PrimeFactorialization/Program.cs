using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeFactorialization
{
    class Program
    {
        static List<int> potentialPrimes;
        static List<int> actualPrimes;
        static HashSet<int> primeFilters = new HashSet<int>();
        static HashSet<int> nonPrimes = new HashSet<int>();
        static Random rand = new Random();
        static int goal;
        static int numRuns = 10;        
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
            potentialPrimes = new List<int>();
            actualPrimes = new List<int>();

            goal = rand.Next(rangeLow, rangeHigh);
            Write(string.Format("Run #{0:n0} Prime Factors of {1:n0}: ", i, goal));
            Factorialize(goal);            
            Clean();
            CalculatePrimes();

            Write(string.Format("{0}", string.Join(", ", actualPrimes)));
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

        static void Clean()
        {
            potentialPrimes = (from d in potentialPrimes select d).Distinct().ToList<int>();
            potentialPrimes.Sort();
        }        

        static void Factorialize(int num)
        {
            if (actualPrimes.IndexOf(num) > -1 || potentialPrimes.IndexOf(num) > - 1 || nonPrimes.Contains(num))
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
