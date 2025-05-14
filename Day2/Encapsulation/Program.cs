class Bank {
    private double _Amount;
    public double Amount {
        get {
            return _Amount;
        }
        set {
            _Amount = value;
        }
    }
}

class Program {
    static void Main(string[] args)
    {
        Bank bank = new Bank();
        bank.Amount = 124155.12;
        Console.WriteLine(bank.Amount);
    }
}
