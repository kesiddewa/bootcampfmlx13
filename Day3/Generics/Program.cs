class Compare {
        public static bool AreEqual<T>(T value1, T value2) => value1.Equals(value2);
}

class Program {
    static void Main(string[] args)
    {
        bool isEqual = Compare.AreEqual<int>(10,10);
        if (isEqual) {
            System.Console.WriteLine("Both are equal");
        } else {
            System.Console.WriteLine("Both are not equal");
        }
    }
}