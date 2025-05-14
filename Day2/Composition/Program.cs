using Motor;
using MotorComponent;
class Program {
    static void Main(string[] args)
    {
        Engine engine = new Engine("250CC", "Daytona");
        Tire tire = new Tire("14", "Cross");
        Motorcycle motorcycle = new Motorcycle(engine, tire);

        Console.WriteLine(motorcycle.engine.engineSize);
        Console.WriteLine(motorcycle.tire.tireBrand);
    }
}