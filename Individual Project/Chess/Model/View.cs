using System.Text;

public class View
{
    public View(){}
    public void RenderView(string[,] board)
    {
        if (board == null) { Console.WriteLine("Data papan permainan kosong."); return; }

        Console.WriteLine("\n  A B C D E F G H");
        Console.WriteLine(" +---------------");
        for (int i = 0; i < board.GetLength(0); i++)
        {
            Console.Write($"{8 - i}|");
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine($"|{8 - i}");
            if (i < board.GetLength(0) - 1)
                Console.WriteLine(" | - - - - - - - -");
        }
        Console.WriteLine(" +---------------");
        Console.WriteLine("  A B C D E F G H\n");
    }
}