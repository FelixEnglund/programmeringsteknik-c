using System;

namespace tgif
{
    class Program
    {
        static void Main(string[] args)
        {
            // Skriv en applikation som läser in ett datum via användarinmatning,
            // som sedan räknar ut hur många dagar det är till nästa fredag.
            //från det inmatade datumet
            //måndag är den första dagen i veckan
            DateTimeOffset userDateTime;

            Console.Write("Skriv dagens datum:");
            Console.ReadLine(userDateTime);

        }
    }
}
