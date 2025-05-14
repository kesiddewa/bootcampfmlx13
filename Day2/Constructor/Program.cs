using System.Drawing;

class Car {
    public string model;
    public string color;
    public int year;

    public Car(string modelName, string modelColor, int modelYear) {
        model = modelName;
        color = modelColor;
        year = modelYear;

    }

    static void Main(string[] args)
    {
        Car Honda = new Car("GTR", "Red", 2012);
        Car Hyundai = new Car("Ioniq", "Black", 2021);
        Console.WriteLine("Model: " + Honda.model + "\nColor: " + Honda.color + "\nYear: " + Honda.year + "\n");
        Console.WriteLine("Model: " + Hyundai.model + "\nColor: " + Hyundai.color + "\nYear: " + Hyundai.year);

    }
}
