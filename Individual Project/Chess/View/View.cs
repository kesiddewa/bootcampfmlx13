using System.Text;

public class View
{
    public View() { }
    public void RenderView(string[,] board)
    {
        if (board == null)
        {
            Console.WriteLine("Data papan permainan kosong.");
            return;
        }

        // Atur encoding console untuk mendukung karakter Unicode
        Console.OutputEncoding = Encoding.UTF8;

        // Karakter Unicode untuk frame papan yang lebih baik
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

        // Lebar setiap sel (termasuk spasi untuk bidak dan padding)
        int cellWidth = 3; // Anda bisa menyesuaikan ini (misalnya, 4 atau 5 untuk lebih besar)

        // Baris header (A-H)
        Console.Write("\n  "); // Spasi awal untuk nomor baris
        for (int j = 0; j < board.GetLength(1); j++)
        {
            Console.Write($" {(char)('A' + j)} ".PadRight(cellWidth + 1));
        }
        Console.WriteLine();

        // Garis atas papan
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

        // Iterasi melalui baris papan
        for (int i = 0; i < board.GetLength(0); i++)
        {
            // Nomor baris (8-1)
            Console.Write($"{8 - i} " + verticalLine);

            // Sel-sel papan
            for (int j = 0; j < board.GetLength(1); j++)
            {
                // Tambahkan padding ke bidak agar berada di tengah sel
                string piece = board[i, j] ?? " "; // Gunakan spasi jika null
                string paddedPiece = $" {piece} "; // Sesuaikan padding jika cellWidth diubah
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

            // Garis pemisah antar baris
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

        // Garis bawah papan
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

        // Baris footer (A-H)
        Console.Write("  "); // Spasi awal untuk nomor baris
        for (int j = 0; j < board.GetLength(1); j++)
        {
            Console.Write($" {(char)('A' + j)} ".PadRight(cellWidth + 1));
        }
        Console.WriteLine("\n");
    }
}