using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int num = Convert.ToInt32(input);
            for (int i = 1; i <= num; i++)
            {
                string output = "";

                if (i % 3 == 0) output += "foo";
                if (i % 4 == 0) output += "baz";
                if (i % 5 == 0) output += "bar";
                if (i % 9 == 0) output += "huzz";
                if (i % 7 == 0) output += "jazz";

                Console.Write((output == "" ? i.ToString() : output) + ", ");
            }
        }
    }
}