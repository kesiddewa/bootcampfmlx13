public class Vehicle
{
    public string Brand = "Generic Vehicle";

    public virtual void Start()
    {
        Console.WriteLine("Vehicle starting...");
    }
}

public class Car : Vehicle
{
    public string Model = "Sedan";

    public override void Start()
    {
        Console.WriteLine("Car starting...");
    }

    public void Drive()
    {
        Console.WriteLine("Car is driving...");
    }
}

public class Program
{
    public static void Main()
    {
        Car myCar = new Car();
        Vehicle vehicleRef = myCar;

        Console.WriteLine("Upcasting:");
        vehicleRef.Start();
        Console.WriteLine(vehicleRef.Brand);

        Console.WriteLine("\nDowncasting:");
        if (vehicleRef is Car)
        {
            Car carRef = (Car)vehicleRef;
            carRef.Start();
            carRef.Drive();
            Console.WriteLine(carRef.Model);
        }
        else
        {
            Console.WriteLine("Downcasting failed.");
        }
    }
}
