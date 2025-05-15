public class Program
{
    static void Main()
    {
        Cuboid obj = new Cuboid(2, 4, 6);
        Console.WriteLine($"Volume is : {obj.Volume()}");
        Console.WriteLine($"Area is : {obj.Area()}");
        Console.WriteLine($"Perimeter is : {obj.Perimeter()}");
    }
}
public class Rectangle
{
    public int length;
    public int breadth;
    public int Area()
    {
        return length * breadth;
    }
    public int Perimeter()
    {
        return 2 * (length + breadth);
    }
}

class Cuboid : Rectangle
{
    public int height;
    public Cuboid(int l, int b, int h)
    {
        length = l;
        breadth = b;
        height = h;
    }
    public int Volume()
    {
        return length * breadth * height;
    }
}