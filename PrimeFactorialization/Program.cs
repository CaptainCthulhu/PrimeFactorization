using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeFactorialization
{
    class Program
    {
        static List<int> potentialPrimes;
        static HashSet<int> eSeive = new HashSet<int>();
        static Random rand = new Random();
        static int goal;
        static int numRuns = 100;
        static int maxSieve = 3;
        static int rangeLow = 100;
        static int rangeHigh = 100000000;

        static void Main(string[] args)
        {
            for (int runs = 1; runs <= numRuns; runs++)
            {
                Run(runs);
            }

            //Console.WriteLine("\nDone!");
            //Console.ReadKey();
        }


        static void Run(int i)
        {
            potentialPrimes = new List<int>();

            goal = rand.Next(rangeLow, rangeHigh);
            Console.WriteLine("Run #{0:n0} Prime Factors of {1:n0}: ", i, goal);
            Factorialize(goal);            
            Clean();
            FillSieve();
            Filter();

            Console.WriteLine(string.Format("{0}", string.Join(", ", potentialPrimes)));
        }

        static void Filter()
        {
            potentialPrimes = potentialPrimes.Intersect(eSeive).ToList<int>();
        }

        static void FillSieve()
        {
            eSeive.Add(2);
            if (maxSieve < potentialPrimes.Max())
            {                
                bool isPrime;                
                for (int i = maxSieve; i <= potentialPrimes.Max(); i += 2)
                {
                    isPrime = true;
                    if (!eSeive.Contains(i))
                    {
                        foreach (int e in eSeive)
                        {
                            if (e <= i && i % e == 0)
                            {
                                isPrime = false;
                                break;
                            }
                        }
                        if (isPrime)
                            eSeive.Add(i);
                    }
                }
                maxSieve = (potentialPrimes.Max() % 2 != 0 ? potentialPrimes.Max() : potentialPrimes.Max() - 1);
            }
        }    

        static void Clean()
        {
            potentialPrimes = (from d in potentialPrimes select d).Distinct().ToList<int>();
            potentialPrimes.Sort();
        }        

        static void Factorialize(int num)
        {
            if (potentialPrimes.IndexOf(num) > -1)
                return;
            else if (num % 2 != 0)
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
