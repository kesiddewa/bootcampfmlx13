namespace Chess;

public class Bishop : IPiece
{
    public bool isAlive { get; set; }
    public Color color { get; set; }
    public ICell position { get; set; }
    public PieceEnum piece { get; set; }
    public int ordinal { get; set; }


    public Bishop(bool isAlive, Color color, ICell position, int ordinal)
    {
        this.isAlive = isAlive;
        this.color = color;
        this.position = position;
        this.piece = PieceEnum.Bishop;
        this.ordinal = ordinal;
    }

    public List<ICell> GetMovePattern()
    {
        var moves = new List<ICell>();
        for (int i = 1; i < 8; i++)
        {
            moves.Add(new Cell(position.row + i, (char)(position.column + i))); // Down-Right
            moves.Add(new Cell(position.row + i, (char)(position.column - i))); // Down-Left
            moves.Add(new Cell(position.row - i, (char)(position.column + i))); // Up-Right
            moves.Add(new Cell(position.row - i, (char)(position.column - i))); // Up-Left
        }
        return moves;
    }

    public bool GetIsAlive() { return isAlive; }
    public void SetIsAlive(bool isAlive) { this.isAlive = isAlive; }
    public Color GetColor() { return color; }
    public PieceEnum GetPieceType() { return piece; }
    public int GetPieceOrdinal() { return ordinal; }
    public ICell GetPosition() { return position; }
    public void SetPosition(ICell position) { this.position = position; }
}