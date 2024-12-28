using System;

class PiRandom
{
    static void Main()
    {
        while (true)
        {
            double Amount;
            Random random = new Random();

            double.TryParse(Console.ReadLine(), out Amount);
            double pie = 0;

            for (int i = 0; i < Amount; i++)
            {
                double x = (random.NextDouble() * 2) - 1;
                double y = (random.NextDouble() * 2) - 1;
                double z = Math.Sqrt(x * x + y * y);

                if (z <= 1)
                {
                    pie += 1;
                }
            }

            Console.WriteLine("Pi:  " + pie / Amount * 4);
            Console.WriteLine("Act: " + "3.14159265359");
        }
    } 
}
