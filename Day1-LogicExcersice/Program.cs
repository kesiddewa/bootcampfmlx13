using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = 100;
            for (int i = 1; i <= num; i++) {
                if (i % 5 == 0 && i % 3 == 0) {
                    Console.WriteLine("foobar, ");
                }
                else if (i % 3 == 0) {
                    Console.WriteLine("foo, ");
                }
                else if (i % 5 == 0) {
                    Console.WriteLine("bar, ");
                }
                else {
                    Console.WriteLine(i + ", ");
                }
            }
        }
    }
}