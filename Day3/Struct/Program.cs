struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Display()
    {
        Console.WriteLine($"Point: ({X}, {Y})");
    }

    public double DistanceFromOrigin()
    {
        return Math.Sqrt(X * X + Y * Y);
    }
}

class Program
{
    static void Main()
    {
        Point p1 = new Point(3, 4);
        p1.Display();
        Console.WriteLine($"Distance from origin: {p1.DistanceFromOrigin()}");

        p1.X = 10;
        p1.Y = 20;
        p1.Display();
    }
}
