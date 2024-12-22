using System;
class PiGrid
{
    static void Main()
    {

        while (true)
        {
            double Amount;
            double.TryParse(Console.ReadLine(), out Amount);

            double pie = 0;

            for (int i = 0; i < Amount; i++)
            {
                for (int j = 0; j < Amount; j++)
                {
                    if (Math.Sqrt(((j / Amount) * (j / Amount)) + ((i / Amount) * (i / Amount))) <= 1)
                    {
                        pie += 1;
                    }
                }
            }
            Console.WriteLine("Pie: " + pie / (Amount * Amount) * 4);
            Console.WriteLine("Act: " + "3.14159265359");
        }
    }
}
