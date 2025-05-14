interface IAnimal {
    void animalSound();
}

class Bird : IAnimal {
    public void animalSound() {
        Console.WriteLine("kugeruk kok...");
    }
}

class Program {
    static void Main(string[] args)
    {
        Bird bird = new Bird();
        bird.animalSound();
    }
}