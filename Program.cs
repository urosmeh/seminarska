using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace seminarska
{
    public static class Program
    {
        static void Main(string[] args)
        {
            List<string> aLengths;
            List<string> bLengths;
            List<string> cLengths;
            int a, b, c;
            int La, Lb, Lc;
            string triangleVals = "";
            string triangleExtensions = "";
            string path = "pr1.txt";

            Console.WriteLine("Filename: ");

            path = Console.ReadLine();
         
            string line = File.ReadLines(path).First();

            string[] inputString = line.Split(' ');

            a = int.Parse(inputString[0].ToString());
            b = int.Parse(inputString[1].ToString());
            c = int.Parse(inputString[2].ToString());

            line = File.ReadLines(path).Skip(1).Take(1).First();

            inputString = line.Split(' ');

            La = int.Parse(inputString[0].ToString());
            Lb = int.Parse(inputString[1].ToString());
            Lc = int.Parse(inputString[2].ToString());
            int comb = (La + 1) * (Lb + 1) * (Lc + 1);
            Console.WriteLine("All possible triangles: " + comb.ToString());
            aLengths = new List<string>();
            bLengths = new List<string>();
            cLengths = new List<string>();

            aLengths.Add(a.ToString());
            bLengths.Add(b.ToString());
            cLengths.Add(c.ToString());

            for (int i = 1; i <= La; i++)
            {
                aLengths.Add((a + i).ToString());
            }

            for (int i = 1; i <= Lb; i++)
            {
                bLengths.Add((b + i).ToString());
            }

            for (int i = 1; i <= Lc; i++)
            {
                cLengths.Add((c + i).ToString());
            }

            var combos = GetAllPossibleCombos(
            new List<List<string>>{
                aLengths,
                bLengths,
                cLengths
            });

            var result = combos.Select(c => string.Join(",", c)).ToList();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            int res = countPossibleBruteForce(result);
            stopwatch.Stop();
            
            
            Console.WriteLine("Possible triangles: " + res);
            Console.WriteLine("Execution Time: " + stopwatch.Elapsed.TotalMilliseconds + "ms");
        }

        public static IEnumerable<IEnumerable<string>> GetAllPossibleCombos(IEnumerable<IEnumerable<string>> strings)
        {
            IEnumerable<IEnumerable<string>> combos = new string[][] { new string[0] };

            foreach (var inner in strings)
                combos = from c in combos
                         from i in inner
                         select c.Append(i);

            return combos;
        }

        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            foreach (TSource element in source)
                yield return element;

            yield return item;
        }

        private static int countPossibleBruteForce(List<string> arr)
        {
            int a, b, c;
            int count = 0;
            double p, area;
            string[] line;
            for(int i = 0; i < arr.Count(); i ++)
            {
                line = arr[i].Split(',');

                a = int.Parse(line[0].ToString());
                b = int.Parse(line[1].ToString());
                c = int.Parse(line[2].ToString());

                p = ((double)(a + b + c) / 2);

                area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
                if(area > 0)
                {
                    count ++;
                }
            }
            return count;
        }
    }
}
