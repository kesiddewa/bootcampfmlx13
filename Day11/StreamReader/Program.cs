using System;
using System.IO;
namespace FileHandlinDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // //Creating a new input stream i.e.  MyFile.txt and opens it
            // StreamReader sr = new StreamReader(@"C:\Users\Kesid Dewa\MyFile.txt");

            // Console.WriteLine("Content of the File");

            // // This is used to specify from where to start reading input stream
            // sr.BaseStream.Seek(0, SeekOrigin.Begin);

            // // To read line from input stream
            // string str = sr.ReadLine();

            // // To read the whole file line by line
            // while (str != null)
            // {
            //     Console.WriteLine(str);
            //     str = sr.ReadLine();
            // }
            // Console.ReadLine();

            // // to close the stream
            // sr.Close();
            // Console.ReadKey();



            string FilePath = @"C:\Users\Kesid Dewa\MyFil.txt";
            string data;
            FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                data = streamReader.ReadToEnd();
            }
            Console.WriteLine(data);
            Console.ReadLine();
        }
    }
}