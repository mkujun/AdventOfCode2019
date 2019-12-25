using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void LoopThroughRange(int r1, int r2)
        {
            Dictionary<int, int[]> keyValuePairs = new Dictionary<int, int[]>();

            for (int i = r1; i <= r2; i++)
            {
                int[] digits = GetDigits(i);

                if (IncreaseDigitRule(digits) && HasDoubleNotLargerGroup(digits))
                {
                    keyValuePairs.Add(i, digits);
                }
            }

        }
        static int[] GetDigits(int num)
        {
            List<int> digits = new List<int>();
            while (num > 0)
            {
                digits.Add(num % 10);
                num = num / 10;
            }
            digits.Reverse();
            return digits.ToArray();
        }

        static bool IncreaseDigitRule(int[] digits)
        {
            for (int i = 1; i < digits.Length; i++)
            {
                if (digits[i] < digits[i - 1]) return false;
            }

            return true;
        }

        // has double, part of a larger group (part 1)
        static bool HasDouble(int[] digits)
        {
            for (int i = 1; i < digits.Length; i++)
            {
                if (digits[i] == digits[i - 1]) return true;
            }

            return false;
        }

        // part 2
        static bool HasDoubleNotLargerGroup(int[] digits)
        {
            var dictionary = new Dictionary<int, int>();

            foreach (var number in digits.GroupBy(x => x))
            {
                dictionary.Add(number.Key, number.Count());
            }

            foreach (var item in dictionary)
            {
                if(item.Value == 2)
                {
                    return true;
                }
            }

            return false;
        }

        static void Main(string[] args)
        {
            LoopThroughRange(236491, 713787);
        }
    }
}
