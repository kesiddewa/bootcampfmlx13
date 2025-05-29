using Chess;

public class King : IPiece
{
    public bool isAlive { get; set; }
    public Color color { get; set; }
    public Cell position { get; set; }
    public PieceEnum piece { get; set; }
    public int ordinal { get; set; }
    public bool isCanCastling { get; set; }


    public King(bool isAlive, Color color, Cell position, int ordinal)
    {
        this.isAlive = isAlive;
        this.color = color;
        this.position = position;
        this.piece = PieceEnum.King; // e.g., PieceEnum.King
        this.ordinal = ordinal;
        this.isCanCastling = true;
    }

    public List<Cell> GetMovePattern()
    {
        var moves = new List<Cell>();
        int[] rowMoves = { 1, 1, 1, 0, 0, -1, -1, -1 };
        int[] colMoves = { 1, 0, -1, 1, -1, 1, 0, -1 };
        for (int i = 0; i < rowMoves.Length; i++)
        {
            moves.Add(new Cell(position.row + rowMoves[i], (char)(position.column + colMoves[i])));
        }
        return moves;
    }

    public bool GetIsAlive() => isAlive;
    public void SetIsAlive(bool isAlive) => this.isAlive = isAlive;
    public Color GetColor() => color;
    public PieceEnum GetPieceType() => piece;
    public int GetPieceOrdinal() => ordinal;
    public Cell GetPosition() => position;
    public void SetPosition(Cell position) => this.position = position;
}