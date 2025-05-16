class Program
{
    static void Main(string[] args)
    {
        Car car1 = new Car("Ford");
        car1.Display();
    }
}

public class Car
{
    public string make;
    public string model;

    public Car(string make) : this(make, "Unknown")
    {

    }

    public Car(string make, string model)
    {
        this.make = make;
        this.model = model;
    }

    public void Display()
    {
        System.Console.WriteLine($"Maker: {make}, Model: {model}");
    }
}