using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Day8
{
    class Program
    {
        static int[] readInputNumbers(string path)
        {
            char[] numbers = System.IO.File.ReadAllText(path).ToCharArray();

            return Array.ConvertAll(numbers.Take(numbers.Count() - 1).ToArray(), c => (int)Char.GetNumericValue(c));
        }

        // part 1
        static int fewestZeroDigits(int [] numbers)
        {
            // global big numbers when comparing layers
            int minZero = 100;
            int totalOnes = 100;
            int totalTwos = 100;

            // 150 (1 layer) = 25 * 6 (image format)
            for (int k = 0; k < numbers.Length; k = k + 150)
            {
                int zeros = 0;
                int ones = 0;
                int twos = 0;

                for (int i = k; i < k + 150; i++)
                {
                    if (numbers[i] == 0) { zeros++; }
                    else if (numbers[i] == 1) { ones++; }
                    else if (numbers[i] == 2) { twos++; }
                }

                if ((zeros != 0) && (zeros < minZero))
                {
                    minZero = zeros;
                    totalOnes = ones;
                    totalTwos = twos;
                }

            }

            return totalOnes * totalTwos;
        }

        // part 2
        static int[] imagePixels(int [] numbers)
        {
            int[] pixels = numbers;

            List<int[]> layers = new List<int[]>();

            // separate all the layers from input
            for (int i = 0; i <= 14850; i = i + 150)
            {
                int counter = 0;

                int[] layer = new int[150];

                for (int j = i; j < (i + 150); j++)
                {
                    layer[counter] = pixels[j];
                    counter++;
                }

                layers.Add(layer);
            }

            int[] firstLayer = layers.First();

            foreach (var item in layers.Skip(1))
            {
                for (int i = 0; i < 150; i++)
                {
                    if (firstLayer[i] == 2)
                    {
                        if (item[i] == 1 || item[i] == 0)
                        {
                            firstLayer[i] = item[i];
                        }
                    }
                }
            }

            // printing out the result
            for (int j = 0; j <= 125; j = j + 25)
            {
                for (int i = j; i < (j + 25); i++)
                {
                   if (firstLayer[i] == 1)
                   {
                       Console.Write("# ");
                   }
                   else if (firstLayer[i] == 0)
                   {
                       Console.Write("  ");
                   }
                }
                Console.WriteLine();
            }

            return null;
        }


        static void Main(string[] args)
        {
            int[] inputNumbers = readInputNumbers(@"path to file");
            imagePixels(inputNumbers);
        }
    }
}
