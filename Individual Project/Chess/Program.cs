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
        GameController game = new GameController(gamePlayers, gamePieces, board);


        View gameView = new View();
        game.StartGame();
        gameView.RenderView(game.board.board);
    }
}