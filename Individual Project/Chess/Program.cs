using Chess;

public class Program
{
    static void Main(string[] args)
    {
        List<IPlayer> gamePlayers = new List<IPlayer>
        {
            new Player(Color.White, Status.Normal),
            new Player(Color.Black, Status.Normal)
        };
        IBoard board = new Board(8, 8);
        List<IPiece> gamePieces = new List<IPiece>();
        SetupInitialPieces(gamePieces);
        GameController game = new GameController(gamePlayers, gamePieces, board);


        View gameView = new View();
        game.StartGame();
        gameView.RenderView(game.board.board);
    }
    public static void SetupInitialPieces(List<IPiece> pieces)
    {
        pieces.Add(new Rook(true, Color.White, new Cell(1, 'A'), 1));
        pieces.Add(new Knight(true, Color.White, new Cell(1, 'B'), 1));
        pieces.Add(new Bishop(true, Color.White, new Cell(1, 'C'), 1));
        pieces.Add(new Queen(true, Color.White, new Cell(1, 'D'), 1));
        pieces.Add(new King(true, Color.White, new Cell(1, 'E'), 1));
        pieces.Add(new Bishop(true, Color.White, new Cell(1, 'F'), 2));
        pieces.Add(new Knight(true, Color.White, new Cell(1, 'G'), 2));
        pieces.Add(new Rook(true, Color.White, new Cell(1, 'H'), 2));
        for (char c = 'A'; c <= 'H'; c++)
            pieces.Add(new Pawn(true, Color.White, new Cell(2, c), c - 'A' + 1));

        pieces.Add(new Rook(true, Color.Black, new Cell(8, 'A'), 1));
        pieces.Add(new Knight(true, Color.Black, new Cell(8, 'B'), 1));
        pieces.Add(new Bishop(true, Color.Black, new Cell(8, 'C'), 1));
        pieces.Add(new Queen(true, Color.Black, new Cell(8, 'D'), 1));
        pieces.Add(new King(true, Color.Black, new Cell(8, 'E'), 1));
        pieces.Add(new Bishop(true, Color.Black, new Cell(8, 'F'), 2));
        pieces.Add(new Knight(true, Color.Black, new Cell(8, 'G'), 2));
        pieces.Add(new Rook(true, Color.Black, new Cell(8, 'H'), 2));
        for (char c = 'A'; c <= 'H'; c++)
            pieces.Add(new Pawn(true, Color.Black, new Cell(7, c), c - 'A' + 1));
    }
}