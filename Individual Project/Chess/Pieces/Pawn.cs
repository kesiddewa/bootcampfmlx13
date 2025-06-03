namespace Chess;

public class Pawn : IPiece
{
    public bool isAlive { get; set; }
    public Color color { get; set; }
    public Cell position { get; set; }
    public PieceEnum piece { get; set; }
    public int ordinal { get; set; }
    public bool isFirstMove { get; set; }
    public bool isCanEnPassant { get; set; }


    public Pawn(bool isAlive, Color color, Cell position, int ordinal)
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
    public List<Cell> GetMovePattern()
    {
        var moves = new List<Cell>();
        int direction = color == Color.White ? 1 : -1;
        Cell forward = new Cell(position.row + direction, position.column);
        if (!pieces.Any(p => p.GetPosition().Equals(forward) && p.GetIsAlive()))
        {
            moves.Add(forward);
        }
        if (isFirstMove)
        {
            Cell forward2 = new Cell(position.row + 2 * direction, position.column);
            if (!pieces.Any(p => p.GetPosition().Equals(forward2) && p.GetIsAlive() &&
                !pieces.Any(p => p.GetPosition().Equals(forward) && p.GetIsAlive())))
            {
                moves.Add(forward2);
            }
        }
        moves.Add(new Cell(position.row + direction, (char)(position.column + 1)));
        moves.Add(new Cell(position.row + direction, (char)(position.column - 1)));
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