using Chess;

public class IPiece
{
    bool IsAlive { get; }
    Color Color { get; }
    Cell Position { get; set; }
    PieceEnum Piece { get; }
    int Ordinal { get; }

    List<Cell> GetMovePattern();
    bool GetIsAlive();
    void SetIsAlive(bool isAlive);
    Color GetColor();
    PieceEnum GetPieceType();
    int GetPieceOrdinal();
    Cell GetPosition();
    void SetPosition(Cell position);

}