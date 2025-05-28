namespace Chess;
public interface IPiece
{
    bool isAlive { get; set; }
    Color color { get; set; }
    Cell position { get; set; }
    PieceEnum piece { get; set; }
    int ordinal { get; set; }

    List<Cell> GetMovePattern();
    bool GetIsAlive();
    void SetIsAlive(bool isAlive);
    Color GetColor();
    PieceEnum GetPieceType();
    int GetPieceOrdinal();
    Cell GetPosition();
    void SetPosition(Cell position);

}