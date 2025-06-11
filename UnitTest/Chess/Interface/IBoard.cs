namespace Chess;

public interface IBoard
{
    string[,] board { get; set; }
    string[,] GetBoard();
    void SetBoard(List<IPiece> pieces);
    
}