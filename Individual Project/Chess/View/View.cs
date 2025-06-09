using System.Text;

public class View
{
    public View() { }
    public void RenderView(string[,] board)
    {
        if (board == null)
        {
            Console.WriteLine("Board data is empty.");
            return;
        }

        // Encoding output
        Console.OutputEncoding = Encoding.UTF8;

        char horizontalLine = '─';
        char verticalLine = '│';
        char topLeftCorner = '┌';
        char topRightCorner = '┐';
        char bottomLeftCorner = '└';
        char bottomRightCorner = '┘';
        char topTee = '┬';
        char bottomTee = '┴';
        char leftTee = '├';
        char rightTee = '┤';
        char cross = '┼';

        int cellWidth = 3;

        // Header (A-H)
        Console.Write("\n  ");
        for (int j = 0; j < board.GetLength(1); j++)
        {
            Console.Write($" {(char)('A' + j)} ".PadRight(cellWidth + 1));
        }
        Console.WriteLine();

        // Top border
        Console.Write("  " + topLeftCorner);
        for (int j = 0; j < board.GetLength(1); j++)
        {
            Console.Write(new string(horizontalLine, cellWidth));
            if (j < board.GetLength(1) - 1)
            {
                Console.Write(topTee);
            }
        }
        Console.WriteLine(topRightCorner);

        // Iterate through each row
        for (int i = 0; i < board.GetLength(0); i++)
        {
            // Row number (8-1)
            Console.Write($"{8 - i} " + verticalLine);

            // Board cells
            for (int j = 0; j < board.GetLength(1); j++)
            {
                string piece = board[i, j] ?? " ";
                string paddedPiece = $" {piece} ";
                if (paddedPiece.Length > cellWidth)
                {
                    paddedPiece = paddedPiece.Substring(0, cellWidth);
                }
                else
                {
                    paddedPiece = paddedPiece.PadRight(cellWidth);
                }
                Console.Write(paddedPiece);
                Console.Write(verticalLine);
            }
            Console.WriteLine($" {8 - i}");

            // Border separator
            if (i < board.GetLength(0) - 1)
            {
                Console.Write("  " + leftTee);
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(new string(horizontalLine, cellWidth));
                    if (j < board.GetLength(1) - 1)
                    {
                        Console.Write(cross);
                    }
                }
                Console.WriteLine(rightTee);
            }
        }

        // Bottom border
        Console.Write("  " + bottomLeftCorner);
        for (int j = 0; j < board.GetLength(1); j++)
        {
            Console.Write(new string(horizontalLine, cellWidth));
            if (j < board.GetLength(1) - 1)
            {
                Console.Write(bottomTee);
            }
        }
        Console.WriteLine(bottomRightCorner);

        // Footer (A-H)
        Console.Write("  ");
        for (int j = 0; j < board.GetLength(1); j++)
        {
            Console.Write($" {(char)('A' + j)} ".PadRight(cellWidth + 1));
        }
        Console.WriteLine("\n");
    }
}