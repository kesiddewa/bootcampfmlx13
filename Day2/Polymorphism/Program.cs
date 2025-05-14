class Animal {
    public virtual void Sound(){
        Console.WriteLine("mbek mbeekk..");
    } 
}

class Cow : Animal {
    public override void Sound()
    {
        Console.WriteLine("moo moo..");
    }
}

class Program {
    static void Main(string[] args)
    {
        Cow cow = new Cow();
        cow.Sound();
    }
}
