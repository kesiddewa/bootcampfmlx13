namespace Chess;
public interface IPiece
{
    bool isAlive { get; set; }
    Color color { get; set; }
    ICell position { get; set; }
    PieceEnum piece { get; set; }
    int ordinal { get; set; }

    List<ICell> GetMovePattern();
    bool GetIsAlive();
    void SetIsAlive(bool isAlive);
    Color GetColor();
    PieceEnum GetPieceType();
    int GetPieceOrdinal();
    ICell GetPosition();
    void SetPosition(ICell position);

}