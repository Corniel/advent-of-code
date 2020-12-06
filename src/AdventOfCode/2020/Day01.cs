﻿namespace AdventOfCode._2020
{
    public static class Day01
    {
        public const int Year = 2020;
        
        public static int One(string input)
        {
            var numbers = UniqueNumbers.Parse(input);
            foreach (var number0 in numbers.Range(max: Year / 2))
            {
                var number1 = Year - number0;
                if(numbers.Contains(number1))
                {
                    return number0 * number1;
                }
            }
            throw new NoAnswer();
        }

        public static long Two(string input)
        {
            var numbers = UniqueNumbers.Parse(input);
            foreach (var number0 in numbers.Range(max: Year / 3))
            {
                foreach (var number1 in numbers.Range(min: number0 + 1, max: (Year - number0) / 2))
                {
                    var number2 = Year - number0 - number1;
                    if (numbers.Contains(number2))
                    {
                        return (long)number0 * (long)number1 * (long)number2;
                    }
                }
            }
            throw new NoAnswer();
        }
    }
}
