using System;
using System.IO;
namespace FileHandlinDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // // Code for user input

            // // This will create a file named MyFile2.txt at the specified location i.e. in the D Drive
            // // Here we are using the StreamWriter constructor which takes the string path as an argument to create an instance of StreamWriter class
            // StreamWriter sw = new StreamWriter(@"C:\Users\Kesid Dewa\MyFile2.txt");

            // //Asking user to enter the text that we want to write into the MyFile.txt file
            // Console.WriteLine("Enter the Text that you want to write on File");

            // // To read the input from the user
            // string str = Console.ReadLine();

            // // To write the data into the stream
            // sw.Write(str);

            // // Clears all the buffers
            // sw.Flush();

            // // To close the stream
            // sw.Close();
            // Console.ReadKey();


            // Code for save variable data into file
            string file = @"C:\Users\Kesid Dewa\MyTextFile1.txt";
            int a, b, result;
            a = 15;
            b = 20;
            result = a + b;

            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write("Sum of {0} + {1} = {2}", a, b, result);
            }
            Console.WriteLine("Saved");
            Console.ReadKey();
        }
    }
}