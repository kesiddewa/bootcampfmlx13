namespace Chess;

public class Board : IBoard
{
    public string[,] board { get; set; }
    public Board(int row, int column)
    {
        board = new string[row, column];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                board[i, j] = "  ";
            }
        }
    }

    public string[,] GetBoard() => board;

    public void SetBoard(List<IPiece> pieces)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                board[i, j] = " ";
            }
        }

        if (pieces == null) return;

        foreach (var piece in pieces)
        {
            if (!piece.GetIsAlive()) continue;
            var pos = piece.GetPosition();

            PieceEnum pieceType = piece.GetPieceType();
            Color pieceColor = piece.GetColor();
            string unicodePieceChar = " ";

            switch (pieceColor)
            {
                case Color.White: // Menggunakan PieceColor
                    switch (pieceType)
                    {
                        case PieceEnum.King: unicodePieceChar = "♔"; break; // Menggunakan PieceType
                        case PieceEnum.Queen: unicodePieceChar = "♕"; break;
                        case PieceEnum.Rook: unicodePieceChar = "♖"; break;
                        case PieceEnum.Bishop: unicodePieceChar = "♗"; break;
                        case PieceEnum.Knight: unicodePieceChar = "♘"; break;
                        case PieceEnum.Pawn: unicodePieceChar = "♙"; break;
                    }
                    break;
                case Color.Black: // Menggunakan PieceColor
                    switch (pieceType)
                    {
                        case PieceEnum.King: unicodePieceChar = "♚"; break; // Menggunakan PieceType
                        case PieceEnum.Queen: unicodePieceChar = "♛"; break;
                        case PieceEnum.Rook: unicodePieceChar = "♜"; break;
                        case PieceEnum.Bishop: unicodePieceChar = "♝"; break;
                        case PieceEnum.Knight: unicodePieceChar = "♞"; break;
                        case PieceEnum.Pawn: unicodePieceChar = "♟"; break;
                    }
                    break;
            }

            int arrayRow = 8 - pos.row;
            int arrayCol = pos.column - 'A';

            if (arrayRow >= 0 && arrayRow < board.GetLength(0) &&
                arrayCol >= 0 && arrayCol < board.GetLength(1))
            {
                board[arrayRow, arrayCol] = unicodePieceChar;
            }
            else
            {
                Console.WriteLine($"Peringatan: Bidak {pieceType} {pieceColor} berada di posisi tidak valid ({pos.row},{pos.column})");
            }
        }
    }
}