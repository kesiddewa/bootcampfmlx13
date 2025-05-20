foreach (char a in "apple")
{
    System.Console.Write(a + " ");
}

System.Console.WriteLine();

var dict = new Dictionary<int, string>()
{
    {7, "tujuh"},
    {1, "satu"}
};

foreach (var item in dict)
{
    System.Console.WriteLine(item.Key);
    System.Console.WriteLine(item.Value);
}