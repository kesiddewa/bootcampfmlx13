using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = 107;
            for (int i = 1; i <= num; i++)
            {
                if (i % 5 == 0 && i % 3 == 0)
                {
                    if (i % 7 == 0)
                    {
                        System.Console.Write("foobarjazz, ");
                    }
                    else
                    {

                        Console.Write("foobar, ");
                    }
                }

                else if (i % 3 == 0)
                {
                    if (i % 7 == 0 && i % 3 == 0)
                    {
                        System.Console.Write("foojazz, ");
                    }
                    else
                    {

                        Console.Write("foo, ");
                    }
                }
                else if (i % 5 == 0)
                {
                    if (i % 7 == 0 && i % 5 == 0)
                    {
                        System.Console.Write("barjazz, ");
                    }
                    else
                    {

                        Console.Write("bar, ");
                    }
                }
                else if (i % 7 == 0)
                {
                    Console.Write("jazz, ");

                }
                else
                {
                    Console.Write(i + ", ");
                }
            }
        }
    }
}