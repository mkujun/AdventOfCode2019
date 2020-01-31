using System;
using System.Collections.Generic;
using System.IO;

namespace Day1
{
    class Program
    {
        // part 1
        static int TankMass(int input)
        {
            int mass = 0;

            return mass += (input / 3) - 2;
        }

        // part 2
        static int FuelMass(int input, int fuelMassByTank)
        {
            int mass = 0;

            mass += (input / 3) - 2;

            if (mass <= 0)
            {
                return fuelMassByTank;
            }

            return FuelMass(mass, fuelMassByTank + mass);
        }

        static string[] ReadFile(string path)
        {
            if (File.Exists(path))
            {
                string[] input = File.ReadAllLines(path);

                return input;
            }

            return null;
        }

        static List<int> ConvertStringArrayToIntList(string[] stringArray)
        {
            List<int> intList = new List<int>();

            if (stringArray != null)
            {
                foreach (var item in stringArray)
                {
                    intList.Add(Int32.Parse(item));
                }
            }

            return intList;
        }

        static void Main(string[] args)
        {
            string[] stringInput = ReadFile("your path to file...");
            List<int> intInput = ConvertStringArrayToIntList(stringInput);

            int fuelMass = 0;

            foreach (var tank in intInput)
            {
                fuelMass += (FuelMass(tank, 0));
            }

            int tankMass = 0;

            foreach (var tank in intInput)
            {
                tankMass += TankMass(tank);
            }

            Console.WriteLine("Hello Day 1!");
        }
    }
}
