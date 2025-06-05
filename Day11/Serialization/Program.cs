using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;


// A class to serialize and deserialize
[Serializable]
public class Tutorial
{
    public int ID { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Create an object of Tutorial class
        Tutorial t1 = new Tutorial();
        t1.ID = 1;
        t1.Name = ".Net";

        // Create a file stream to write the object
        FileStream fs = new FileStream("Example.json", FileMode.Create);

        // Create an XML serializer to serialize the object
        JsonSerializer.Serialize(fs, t1);
        // Serialize the object and write it to the file stream
        // Close the file stream
        fs.Close();

        // Create another file stream to read the object
        FileStream fs2 = new FileStream("Example.json", FileMode.Open);

        // Create another XML serializer to deserialize the object

        // Deserialize the object and cast it to Tutorial class
        Tutorial t2 = (Tutorial)JsonSerializer.Deserialize(fs2, typeof(Tutorial));

        // Close the file stream
        fs2.Close();

        // Print the properties of the deserialized object
        Console.WriteLine("ID: {0}", t2.ID);
        Console.WriteLine("Name: {0}", t2.Name);
    }
}
