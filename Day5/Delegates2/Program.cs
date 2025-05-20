class Program
{
    public delegate void AddNum(int a, int b);
    public delegate void SubNum(int a, int b);

    public void sum(int a, int b)
    {
        int c = a + b;
        System.Console.WriteLine($"{a} + {b} = {c}");
    }

    public void subtract(int a, int b)
    {
        int c = a + b;
        System.Console.WriteLine($"{a} - {b} = {c}");
    }

    static void Main(string[] args)
    {
        Program obj = new Program();
        AddNum del_obj1 = new AddNum(obj.sum);
        SubNum del_obj2 = new SubNum(obj.subtract);

        del_obj1(123, 89);
        del_obj2(56, 12);
    }
}