class Program {
    enum Gender {
        Male,
        Female,
        Unknown
    }

    static void Main(string[] args)
    {
        Gender gender = Gender.Female;
        switch(gender) {
            case Gender.Male: 
                System.Console.WriteLine("kamu laki-laki bro");
                break;
            case Gender.Female:
                System.Console.WriteLine("kamu perempuan sist");
                break;
            case Gender.Unknown:
                System.Console.WriteLine("hmmm....");
                break;
        }
    }

}
