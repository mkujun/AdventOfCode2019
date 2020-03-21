using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    public static class HelperComputer
    {
        public static int[] StringCSVtoArray(string input)
        {
            string[] data = input.Split(',').ToArray();

            int[] ints = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                ints[i] = Int32.Parse(data[i]);
            }

            return ints;
        }
        public static int[] SplitDigits(int number)
        {
            List<int> digits = new List<int>();

            var digitCollection = number.ToString().Select(c => Int32.Parse(c.ToString()));

            foreach (var digit in digitCollection)
            {
                digits.Add(digit);
            }

            return digits.ToArray();
        }
        
    }

    public class IntcodeComputer
    {
        private int[] Data;
        private int Pointer;

        public void LoadData(int[] data)
        {
            Data = data;
        }

        public Tuple<int,int> DefineMode(Tuple<int,int> first, Tuple<int,int> second)
        {
            int one = (first.Item1 == 0) ? Data[first.Item2] : first.Item2;
            int two = (second.Item1 == 0) ? Data[second.Item2] : second.Item2;

            return new Tuple<int, int>(one, two);
        }

        public void Ins1(Tuple<int, int> first, Tuple<int,int> second, int destination)
        {
            Tuple<int, int> modeParameters = DefineMode(first, second);

            Data[destination] = modeParameters.Item1 + modeParameters.Item2;

            Pointer = Pointer + 4;
            TEST(Pointer);
        }
        public void Ins2(Tuple<int, int> first, Tuple<int,int> second, int destination)
        {
            Tuple<int, int> modeParameters = DefineMode(first, second);

            Data[destination] = modeParameters.Item1 * modeParameters.Item2;

            Pointer = Pointer + 4;
            TEST(Pointer);
        }
        public void Ins3(int parameter)
        {
            Data[parameter] = 1;

            Pointer = Pointer + 2;
            TEST(Pointer);
        }
        public void Ins4(int parameter)
        {
            Console.WriteLine("Output at the address { " + parameter + "is" + Data[parameter] + " }");

            Pointer = Pointer + 2;
            TEST(Pointer);
        }

        public Dictionary<string, int> SetParameters(int[] parameters)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            dict.Add("E", parameters[parameters.Length - 1]);
            dict.Add("D", parameters[parameters.Length - 2]);
            dict.Add("C", parameters[parameters.Length - 3]);
            dict.Add("B", 0);
            dict.Add("A", 0);

            if (parameters.Length == 4)
                dict["B"] = parameters[parameters.Length - 4];
            if (parameters.Length == 5)
                dict["A"] = parameters[parameters.Length - 5];

            return dict;
        }

        public void ParameterMode(int instruction, int firstParameter, int secondParameter, int destination)
        {
            int[] parameters = HelperComputer.SplitDigits(instruction);
            Dictionary<string, int> keyValuePairs = SetParameters(parameters);

            switch (keyValuePairs["E"])
            {
                case 1:
                    Ins1(new Tuple<int, int>(keyValuePairs["C"], firstParameter), new Tuple<int, int>(keyValuePairs["B"], secondParameter), destination);
                    break;
                case 2:
                    Ins2(new Tuple<int, int>(keyValuePairs["C"], firstParameter), new Tuple<int, int>(keyValuePairs["B"], secondParameter), destination);
                    break;
                case 3:
                    Ins3(firstParameter);
                    break;
                case 4:
                    Ins4(firstParameter);
                    break;
                default:
                    break;
            }
        }

        public void TEST(int pointer)
        {
            Pointer = pointer;
            switch (Data[Pointer])
            {
                case 1:
                    Ins1(new Tuple<int, int>(0, Data[Pointer + 1]), new Tuple<int, int>(0, Data[Pointer + 2]), Data[Pointer + 3]);
                    break;
                case 2:
                    Ins2(new Tuple<int, int>(0, Data[Pointer + 1]), new Tuple<int, int>(0, Data[Pointer + 2]), Data[Pointer + 3]);
                    break;
                case 3:
                    Ins3(Data[Pointer + 1]);
                    break;
                case 4:
                    Ins4(Data[Pointer + 1]);
                    break;
                case 99:
                    break;
                default:
                    ParameterMode(Data[Pointer], Data[Pointer + 1], Data[Pointer + 2], Data[Pointer + 3]);
                    break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // start with input "1" to the program
            IntcodeComputer intcodeComputer = new IntcodeComputer();
            string input = "3,225,1,225,6,6,1100,1,238,225,104,0,1101,78,5,225,1,166,139,224,101,-74,224,224,4,224,1002,223,8,223,1001,224,6,224,1,223,224,223,1002,136,18,224,101,-918,224,224,4,224,1002,223,8,223,101,2,224,224,1,224,223,223,1001,83,84,224,1001,224,-139,224,4,224,102,8,223,223,101,3,224,224,1,224,223,223,1102,55,20,225,1101,53,94,225,2,217,87,224,1001,224,-2120,224,4,224,1002,223,8,223,1001,224,1,224,1,224,223,223,102,37,14,224,101,-185,224,224,4,224,1002,223,8,223,1001,224,1,224,1,224,223,223,1101,8,51,225,1102,46,15,225,1102,88,87,224,1001,224,-7656,224,4,224,102,8,223,223,101,7,224,224,1,223,224,223,1101,29,28,225,1101,58,43,224,1001,224,-101,224,4,224,1002,223,8,223,1001,224,6,224,1,224,223,223,1101,93,54,225,101,40,191,224,1001,224,-133,224,4,224,102,8,223,223,101,3,224,224,1,223,224,223,1101,40,79,225,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,1008,226,677,224,1002,223,2,223,1005,224,329,1001,223,1,223,1107,226,677,224,1002,223,2,223,1005,224,344,1001,223,1,223,8,677,226,224,1002,223,2,223,1006,224,359,1001,223,1,223,1108,226,677,224,1002,223,2,223,1006,224,374,101,1,223,223,1007,677,677,224,102,2,223,223,1006,224,389,1001,223,1,223,8,226,677,224,102,2,223,223,1006,224,404,101,1,223,223,1007,226,226,224,1002,223,2,223,1006,224,419,101,1,223,223,107,677,226,224,1002,223,2,223,1006,224,434,1001,223,1,223,1007,226,677,224,102,2,223,223,1005,224,449,101,1,223,223,1107,226,226,224,1002,223,2,223,1005,224,464,1001,223,1,223,107,226,226,224,102,2,223,223,1006,224,479,101,1,223,223,108,226,226,224,1002,223,2,223,1006,224,494,101,1,223,223,107,677,677,224,102,2,223,223,1005,224,509,1001,223,1,223,1008,677,677,224,1002,223,2,223,1006,224,524,101,1,223,223,1107,677,226,224,102,2,223,223,1006,224,539,1001,223,1,223,108,677,226,224,102,2,223,223,1006,224,554,1001,223,1,223,1108,677,226,224,102,2,223,223,1005,224,569,1001,223,1,223,8,677,677,224,1002,223,2,223,1005,224,584,1001,223,1,223,7,677,677,224,1002,223,2,223,1005,224,599,101,1,223,223,1108,226,226,224,102,2,223,223,1006,224,614,101,1,223,223,1008,226,226,224,1002,223,2,223,1005,224,629,101,1,223,223,7,677,226,224,102,2,223,223,1006,224,644,1001,223,1,223,7,226,677,224,102,2,223,223,1005,224,659,101,1,223,223,108,677,677,224,1002,223,2,223,1006,224,674,101,1,223,223,4,223,99,226";
            intcodeComputer.LoadData(HelperComputer.StringCSVtoArray(input));

            intcodeComputer.TEST(0);
        }
    }
}
