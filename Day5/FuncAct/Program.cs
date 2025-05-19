Func<int, int, int> add = (x, y) => x + y;
Console.WriteLine(add(2, 3));

System.Console.WriteLine();

Action<int, int> ActionCalculator = (a, b) =>
{
    Console.WriteLine($"Addition result: {a + b}");
    Console.WriteLine($"Subtraction result: {a - b}");
    Console.WriteLine($"Multiplication result: {a * b}");
    Console.WriteLine($"Division result: {a / b}");
};

ActionCalculator(4, 2);