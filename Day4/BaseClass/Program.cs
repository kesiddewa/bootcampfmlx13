public class Animal
{
    public string Name;

    public Animal(string name)
    {
        Name = name;
    }

    public virtual void Speak()
    {
        Console.WriteLine("Animal speaks...");
    }
}

public class Dog : Animal
{
    public Dog(string name) : base(name) // Memanggil constructor Animal
    {
    }

    public override void Speak()
    {
        base.Speak(); // Memanggil method Speak dari kelas Animal
        Console.WriteLine($"{Name} says: Woof!");
    }
}

public class Program
{
    public static void Main()
    {
        Dog myDog = new Dog("Buddy");
        myDog.Speak();
    }
}
