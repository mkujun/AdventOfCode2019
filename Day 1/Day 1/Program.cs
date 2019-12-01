using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Day_1
{
    class Program
    {
        static int TankMass(int input)
        {
            int mass = 0;

            return mass += (input / 3) - 2;
        }

        static int FuelMass(int input, int fuelMassByTank)
        {
            int mass = 0;

            mass += (input / 3) - 2;

            if(mass <= 0)
            {
                return fuelMassByTank += mass;
            }

            return FuelMass(mass, fuelMassByTank);
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

            foreach (var item in stringArray)
            {
                intList.Add(Int32.Parse(item));
            }

            return intList;
        }

        static void Main(string[] args)
        {
            string [] stringInput = ReadFile("C:\\Projects\\AdventOfCode2019\\Day 1\\Day 1\\input\\input.txt");
            List<int> intInput = ConvertStringArrayToIntList(stringInput);


            /*
            int totalMass = 0;
            foreach (var tank in intInput)
            {
                int tankMass = TankMass(tank);
                int fuelMass = FuelMass(tank, 0);

                totalMass += (tankMass + fuelMass);
                //totalMass += (TankMass(tank) + FuelMass(tank, 0));
            }
            */

            //List<int> testData = new List<int>();
            //testData.Add(12);
            //testData.Add(14);
            //testData.Add(1969);
            //testData.Add(100756);

            int totalMass = 0;
            int tankMass = TankMass(1969);
            int fuelMass = FuelMass(1969, 0);
            //totalMass += (TankMass(1969) + FuelMass(1969, 0));

            Console.WriteLine("Hello World!");
        }
    }
}
