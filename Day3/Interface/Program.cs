interface IVehicle {
    void SpeedUp(int a); 
}

class Bicycle : IVehicle {
    int speed;
    public void SpeedUp(int increment) {
        speed = speed + increment;
    }

    public void CheckSpeed() {
        System.Console.WriteLine("Speed: " + speed);
    }
}

class Bike : IVehicle {
    int speed;
    public void SpeedUp(int increment) {
        speed = speed + increment;
    }

    public void CheckSpeed() {
        System.Console.WriteLine("Speed: " + speed);
    }
}

class Program {
    static void Main(string[] args)
    {
        Bicycle bicycle1 = new Bicycle();
        bicycle1.SpeedUp(3);
        bicycle1.CheckSpeed();
        bicycle1.SpeedUp(5);
        bicycle1.CheckSpeed();
    }
}