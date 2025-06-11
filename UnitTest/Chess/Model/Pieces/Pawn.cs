namespace Chess;

public class Pawn : IPiece
{
    public bool isAlive { get; set; }
    public Color color { get; set; }    
    public ICell position { get; set; }
    public PieceEnum piece { get; set; }
    public int ordinal { get; set; }
    public bool isFirstMove { get; set; }
    public bool isCanEnPassant { get; set; }


    public Pawn(bool isAlive, Color color, ICell position, int ordinal)
    {
        this.isAlive = isAlive;
        this.color = color;
        this.position = position;
        this.piece = PieceEnum.Pawn;
        this.ordinal = ordinal;
        this.isFirstMove = true;
        this.isCanEnPassant = false;
    }

    //should be the correct method to return move for pawn
    public List<ICell> GetMovePattern()
    {
        var moves = new List<ICell>();
        int direction = color == Color.White ? 1 : -1;

        // forward one square
        moves.Add(new Cell(position.row + direction, position.column));

        // Forward two squares on first move
        if (isFirstMove)
        {
            moves.Add(new Cell(position.row + 2 * direction, position.column));
        }

        // Diagonal right
        moves.Add(new Cell(position.row + direction, (char)(position.column + 1)));

        // Diagonal left
        moves.Add(new Cell(position.row + direction, (char)(position.column - 1)));
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