using System;
using System.Collections;
using System.Collections.Generic;

namespace Day6
{
    class Program
    {
        public class Interstellar
        {
            public List<List<string>> Dataset;

            public Interstellar()
            {
                Dataset = new List<List<string>>();
            }

            public void Add(string input)
            {
                string planet = input.Split(')')[0];
                string orbit = input.Split(')')[1];

                if (Dataset.Count == 0)
                {
                    Dataset.Add(new List<string> { planet , orbit });
                    Dataset.Add(new List<string> { orbit });
                }
                else
                {
                    foreach (var planetItem in Dataset)
                    {
                        if (planetItem.Contains(planet))
                        {
                            planetItem.Add(orbit);
                        }
                    }
                    Dataset.Add(new List<string> { orbit });
                }
            }
        }

        static void Main(string[] args)
        {
            Interstellar interstellar = new Interstellar();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\markok\source\repos\AdventOfCode2019\Day6\taskInput.txt");
            while((line = file.ReadLine()) != null)
            {
                interstellar.Add(line);
            }

            int count = 0;
            foreach (var stellar in interstellar.Dataset)
            {
                count = count + (stellar.Count - 1);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
