using System;
using System.IO;
using System.Text;
namespace FileHandlinDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //File Creation
            // string FilePath = @"C:\Users\Kesid Dewa\MyFile.txt";
            // FileStream fileStream = new FileStream(FilePath, FileMode.Create);
            // fileStream.Close();
            // Console.Write("File has been created and the Path is C:\\Users\\Kesid Dewa\\MyFile.txt");
            // Console.ReadKey();

            // File Open and Write
            // string FilePath = @"C:\Users\Kesid Dewa\MyFile.txt";
            // FileStream fileStream = new FileStream(FilePath, FileMode.Append);
            // byte[] bdata = Encoding.Default.GetBytes("C# Is an Object Oriented Programming Language");
            // fileStream.Write(bdata, 0, bdata.Length);
            // fileStream.Close();
            // Console.WriteLine("Successfully saved file with data : C# Is an Object Oriented Programming Language");
            // Console.ReadKey();

            // File Read
            string FilePath = @"C:\Users\Kesid Dewa\MyFile.txt";
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