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
            board[pos.row - 1, pos.column - 'A'] = piece.GetPieceType().ToString()[0].ToString();
        }
    }
}