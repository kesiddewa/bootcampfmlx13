namespace Chess;
public class Bishop : IPiece
{
    public bool isAlive { get; set; }
    public Color color { get; set; }
    public Cell position { get; set; }
    public PieceEnum piece { get; set; }
    public int ordinal { get; set; }


    public Bishop(bool isAlive, Color color, Cell position, int ordinal)
    {
        this.isAlive = isAlive;
        this.color = color;
        this.position = position;
        this.piece = PieceEnum.Bishop; // e.g., PieceEnum.Bishop
        this.ordinal = ordinal;
    }

    public List<Cell> GetMovePattern()
    {
        var moves = new List<Cell>();
        for (int i = 1; i < 8; i++)
        {
            moves.Add(new Cell(position.row + i, (char)(position.column + i))); // Down-Right
            moves.Add(new Cell(position.row + i, (char)(position.column - i))); // Down-Left
            moves.Add(new Cell(position.row - i, (char)(position.column + i))); // Up-Right
            moves.Add(new Cell(position.row - i, (char)(position.column - i))); // Up-Left
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