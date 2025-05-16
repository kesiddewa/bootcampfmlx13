using System;

public class OuterClass
{
    public string Name = "Saya OuterClass";

    public void ShowOuter()
    {
        Console.WriteLine("Method OuterClass.");
    }

    public class NestedClass
    {
        public void ShowNested()
        {
            Console.WriteLine("Method NestedClass.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        OuterClass outer = new OuterClass();
        outer.ShowOuter();
        Console.WriteLine(outer.Name);

        OuterClass.NestedClass nested = new OuterClass.NestedClass();
        nested.ShowNested();
    }
}
