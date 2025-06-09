namespace Chess;
public class Rook : IPiece
{
    public bool isAlive { get; set; }
    public Color color { get; set; }
    public ICell position { get; set; }
    public PieceEnum piece { get; set; }
    public int ordinal { get; set; }

    public Rook(bool isAlive, Color color, ICell position, int ordinal)
    {
        this.isAlive = isAlive;
        this.color = color;
        this.position = position;
        this.piece = PieceEnum.Rook;
        this.ordinal = ordinal;
    }

    public List<ICell> GetMovePattern()
    {
        var moves = new List<ICell>();
        for(int i = 0; i < 8; i++)
        {
            // Horizontal Moves
            moves.Add(new Cell(position.row + i, position.column)); // Down
            moves.Add(new Cell(position.row - i, position.column)); // Up
            moves.Add(new Cell(position.row, (char)(position.column + i))); // Right
            moves.Add(new Cell(position.row, (char)(position.column - i))); // Left
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