class Rectangle
{

    public delegate void rectDelegate(double height,
                                      double width);

    public void Area(double height, double width)
    {
        Console.WriteLine("Area is: {0}", (width * height));
    }

    public void Perimeter(double height, double width)
    {
        Console.WriteLine("Perimeter is: {0} ", 2 * (width + height));
    }


    public static void Main(string[] args)
    {

        Rectangle rect = new Rectangle();

        rectDelegate rectdele = new rectDelegate(rect.Area);

        rectdele += rect.Perimeter;

        rectdele(6.3, 4.2);
        Console.WriteLine();

        rectdele.Invoke(16.3, 10.3);
    }
}
