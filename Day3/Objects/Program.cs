class Program {
    static void Main(string[] args)
    {
        int a = 123;
        object b = a;

        System.Console.WriteLine(b.GetType());

        int x = 567;
        System.Console.WriteLine(x.ToString().GetType());

        string y = "242";
        System.Console.WriteLine(Convert.ToInt64(y).GetType().Name);

        System.Console.WriteLine(x.ToString().GetType().Name);
    }
}