using System;

namespace leapyearcalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            int year1 = int.Parse(args[0]);
            int year2 = int.Parse(args[1]);

            int maxYear = Math.Max(year1, year2);
            int minYear = Math.Min(year1, year2);

            int leapYearCount = 0;
            
            
            for (var year = minYear; year <= maxYear; year++)
            {
                bool isLeapYear = DateTime.IsLeapYear(year);

                if (isLeapYear)
                {
                    leapYearCount++;
                }
            }

            Console.WriteLine($"Encounterd {leapYearCount} leap years from {minYear} to {maxYear}.");
            
            // Räkna ut hur många skottår som passerat mellan två inmatade värden.

            // DateTime.IsLeapYear(year) är en metod man kan använda.
        }
    }
}
