    // int[] arr = new int[3];
    string x = "sad";
try
{
    // arr[3] = 1;

    System.Console.WriteLine(Convert.ToInt32(x));
}
catch (IndexOutOfRangeException)
{
    System.Console.WriteLine("kamu out of range!");
    throw;
}
catch (FormatException)
{
    System.Console.WriteLine("tidak bisa diconvert");
    throw;
}
finally
{
    System.Console.WriteLine(x);
}