class Program {
    static void Main(string[] args)
    {
        Program obj = new Program();
        obj.Add(5, 20);
        obj.Add(12.5f, 19.3f);
        obj.Add("Bunga", "Mawar");
    }

    public void Add(int a, int b) {
        Console.WriteLine(a + b);
    }

    public void Add(float x, float y) {
        Console.WriteLine(x + y);
    }

    public void Add(string s1, string s2) {
        Console.WriteLine(s1 + " " + s2);
    }

    
}

