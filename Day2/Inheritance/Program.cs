class Vehicle {
    public void honk() {
        Console.WriteLine("tuut tuutt!!");
    }
}

class Motorcyle : Vehicle {
    public string modelName = "W175";
    
}

class Program {
    static void Main(string[] args)
    {
        Motorcyle Kawasaki = new Motorcyle();
        Kawasaki.honk();
    }
}