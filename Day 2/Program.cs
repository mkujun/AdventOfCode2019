using System;
using System.Collections.Generic;
using System.IO;

namespace Day_2
{
    class Program
    {
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

        static void Main(string[] args)
        {
            int[] input = ReadFile("path to file");

            int[] inputData = input;
            inputData[1] = 12;
            inputData[2] = 2;

            CalculateOpcode(inputData);

            Console.WriteLine("Hello Day 2 part 1!");
        }
    }
}
