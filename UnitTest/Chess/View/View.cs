using System.Text;
using Chess;

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

    public void ShowNoPieceAtPosition()
    {
        Console.WriteLine("There is no piece at that position.");
    }

    public void ShowNotYourPiece()
    {
        Console.WriteLine("The pawn you selected doesn't belong to you.");
    }

    public void ShowInvalidPieceMove()
    {
        Console.WriteLine("Invalid piece move.");
    }

    public void ShowKingInCheck()
    {
        Console.WriteLine("Invalid piece move: this move puts your king in check.");
    }

    public void ShowCapturedPiece(string pieceType, Color color)
    {
        Console.WriteLine($"{pieceType} {color} player's captured!");
    }

    public void ShowMoveSuccess()
    {
        Console.WriteLine("Move successful!");
    }

    public void ShowCheckmate(Color winner)
    {
        Console.WriteLine($"CHECKMATE! {winner} player win!");
    }

    public void ShowCheck(Color color)
    {
        Console.WriteLine($"CHECK! {color} player's king is in danger!");
    }

    public void ShowPawnPromotionChoice()
    {
        Console.WriteLine("Choose a piece to promote your pawn to ((R) for Rook, (B) for Bishop, (N) for Knight, (Q) for Queen): ");
    }

    public void ShowPawnPromoted(string pieceType, ICell position)
    {
        Console.WriteLine($"Pawn promoted to {pieceType} at {position.column}{position.row}");
    }

    public void ShowEnPassantSuccess()
    {
        Console.WriteLine("En Passant success!");
    }
}