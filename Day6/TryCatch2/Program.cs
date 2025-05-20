string input = "blablabla";
int result;
bool success = int.TryParse(input, out result);

if (success)
{
    Console.WriteLine($"Parsed successfully: {result}");
}
else
{
    Console.WriteLine("Failed to parse the input.");
}