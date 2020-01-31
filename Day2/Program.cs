using System;
using System.Collections.Generic;
using System.IO;

namespace Day2
{
    class Program
    {
        static string FilePath = "path to file";

        static int[] ReadFile(string path)
        {
            if (File.Exists(path))
            {
                string[] input = File.ReadAllText(path).Split(',');

                int[] myInt = Array.ConvertAll(input, s => int.Parse(s));

                return myInt;
            }

            return null;
        }

        static int[] CalculateOpcode(int[] data)
        {
            for (int i = 0; i < data.Length; i = i + 4)
            {
                int firstPosition = data[i + 1];
                int secondPosition = data[i + 2];
                int destination = data[i + 3];

                if (data[i] == 1)
                {
                    int add = data[firstPosition] + data[secondPosition];

                    data[destination] = add;
                }
                if (data[i] == 2)
                {
                    int multiply = data[firstPosition] * data[secondPosition];

                    data[destination] = multiply;
                }
                if (data[i] == 99)
                {
                    return data;
                }
            }

            return data;
        }

        static Tuple<int, int> FindNounAndVerb(int output)
        {
            int[] opcodeResult;

            for (int i = 0; i <= 99; i++)
            {
                for (int j = 0; j <= 99; j++)
                {
                    // call function each time with fresh input
                    int[] data = ReadFile(FilePath);
                    data[1] = i;
                    data[2] = j;
                    opcodeResult = CalculateOpcode(data);

                    if (opcodeResult[0] == output)
                    {
                        return new Tuple<int, int>(opcodeResult[1], opcodeResult[2]);
                    }
                }
            }

            return null;
        }

        static void Main(string[] args)
        {
            // part 1
            int[] input1 = ReadFile(FilePath);
            input1[1] = 12;
            input1[2] = 2;

            CalculateOpcode(input1);

            // part 2
            Tuple<int, int> VerbNoun = FindNounAndVerb(19690720);

            Console.WriteLine("Hello Day 2");
        }
    }
}
