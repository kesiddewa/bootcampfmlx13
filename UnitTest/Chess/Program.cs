using Chess;

public class Program
{
    static void Main(string[] args)
    {
        bool playAgain = true;

        while (playAgain)
        {
            List<IPlayer> gamePlayers = new List<IPlayer>
            {
                new Player(Color.White, Status.Normal),
                new Player(Color.Black, Status.Normal)
            };
            IBoard board = new Board(8, 8);
            List<IPiece> gamePieces = new List<IPiece>();
            View gameView = new View();
            GameController game = new GameController(gamePlayers, gamePieces, board, gameView);
            game.SetupInitialPieces(gamePieces);

            game.StartGame();
            gameView.RenderView(game.board.board);

            while (!game.EndGame())
            {
                Console.WriteLine($"Player turn: {game.players[0].GetColor()}");
                Console.Write("Enter your move (e.g., E2 E4) or type 'exit' to quit: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Quitting the game...");
                    break;
                }

                string[] parts = input.ToUpper().Split(' ');
                if (parts.Length != 2 || parts[0].Length != 2 || parts[1].Length != 2)
                {
                    gameView.RenderView(game.board.board);
                    Console.WriteLine("Invalid input format. Must be in the format 'E2 E4'.");
                    continue;
                }

                ICell fromCell = new Cell(int.Parse(parts[0][1].ToString()), parts[0][0]);
                ICell toCell = new Cell(int.Parse(parts[1][1].ToString()), parts[1][0]);

                if (fromCell.column < 'A' || fromCell.column > 'H' || fromCell.row < 1 || fromCell.row > 8 ||
                    toCell.column < 'A' || toCell.column > 'H' || toCell.row < 1 || toCell.row > 8)
                {
                    Console.WriteLine("Invalid cell coordinates. Must be between A1 and H8.");
                    continue;
                }
                game.PlayerMove(fromCell, toCell);

                gameView.RenderView(game.board.board);

            }

            Console.WriteLine("Game Over!");
            Console.WriteLine($"Final Status: {game.status}");

            Console.Write("Do you want to play again? (y/n): ");
            string playAgainInput = Console.ReadLine().ToLower();
            playAgain = playAgainInput == "y";
        }

        Console.WriteLine("Thank you for playing!");
    }
}