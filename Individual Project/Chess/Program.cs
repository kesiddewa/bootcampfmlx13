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

        while (game.status == Status.Normal || game.status == Status.Check)
        {
            Console.WriteLine($"Player turn: {game.players[0].GetColor()}");
            Console.Write("Masukkan langkah (misal: E2 E4), atau 'exit' untuk keluar: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                Console.WriteLine("Keluar dari permainan.");
                break;
            }

            string[] parts = input.ToUpper().Split(' ');
            if (parts.Length != 2 || parts[0].Length != 2 || parts[1].Length != 2)
            {
                gameView.RenderView(game.board.board);
                Console.WriteLine("Format input tidak valid. Contoh: e2 e4");
                continue;
            }

            try
            {
                Cell fromCell = new Cell(int.Parse(parts[0][1].ToString()), parts[0][0]);
                Cell toCell = new Cell(int.Parse(parts[1][1].ToString()), parts[1][0]);

                // Validasi input sel dasar
                if (fromCell.column < 'A' || fromCell.column > 'H' || fromCell.row < 1 || fromCell.row > 8 ||
                    toCell.column < 'A' || toCell.column > 'H' || toCell.row < 1 || toCell.row > 8)
                {
                    Console.WriteLine("Koordinat sel tidak valid.");
                    continue;
                }
                game.PlayerMove(fromCell, toCell); // Panggil metode yang dimodifikasi
                gameView.RenderView(game.board.board);
            }
            catch (FormatException)
            {
                Console.WriteLine("Format baris pada input sel tidak valid (harus angka).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan: {ex.Message}");
            }
        }
    }
    public static void SetupInitialPieces(List<IPiece> pieces)
    {
        // Default chess pieces setup
        // pieces.Add(new Rook(true, Color.White, new Cell(1, 'A'), 1));
        // pieces.Add(new Knight(true, Color.White, new Cell(1, 'B'), 1));
        // pieces.Add(new Bishop(true, Color.White, new Cell(1, 'C'), 1));
        // pieces.Add(new Queen(true, Color.White, new Cell(1, 'D'), 1));
        // pieces.Add(new King(true, Color.White, new Cell(1, 'E'), 1));
        // pieces.Add(new Bishop(true, Color.White, new Cell(1, 'F'), 2));
        // pieces.Add(new Knight(true, Color.White, new Cell(1, 'G'), 2));
        // pieces.Add(new Rook(true, Color.White, new Cell(1, 'H'), 2));
        // for (char c = 'A'; c <= 'H'; c++)
        //     pieces.Add(new Pawn(true, Color.White, new Cell(2, c), c - 'A' + 1));

        // pieces.Add(new Rook(true, Color.Black, new Cell(8, 'A'), 1));
        // pieces.Add(new Knight(true, Color.Black, new Cell(8, 'B'), 1));
        // pieces.Add(new Bishop(true, Color.Black, new Cell(8, 'C'), 1));
        // pieces.Add(new Queen(true, Color.Black, new Cell(8, 'D'), 1));
        // pieces.Add(new King(true, Color.Black, new Cell(8, 'E'), 1));
        // pieces.Add(new Bishop(true, Color.Black, new Cell(8, 'F'), 2));
        // pieces.Add(new Knight(true, Color.Black, new Cell(8, 'G'), 2));
        // pieces.Add(new Rook(true, Color.Black, new Cell(8, 'H'), 2));
        // for (char c = 'A'; c <= 'H'; c++)
        //     pieces.Add(new Pawn(true, Color.Black, new Cell(7, c), c - 'A' + 1));



        // Custom pieces setup for testing

        // Promote pawn scenario
        // pieces.Add(new Pawn(true, Color.White, new Cell(7, 'A'), 1));
        // pieces.Add(new Pawn(true, Color.Black, new Cell(7, 'H'), 1));

        // Castling scenario
        // pieces.Add(new King(true, Color.Black, new Cell(8, 'E'), 1));
        // pieces.Add(new Rook(true, Color.Black, new Cell(8, 'H'), 2));
        // pieces.Add(new King(true, Color.White, new Cell(1, 'E'), 1));
        // pieces.Add(new Rook(true, Color.White, new Cell(1, 'A'), 1));

        // En Passant scenario
        // pieces.Add(new Pawn(true, Color.White, new Cell(4, 'D'), 1));
        // pieces.Add(new Pawn(true, Color.Black, new Cell(5, 'E'), 1));

        // Check scenario
        // pieces.Add(new Rook(true, Color.White, new Cell(7, 'A'), 1));
        // pieces.Add(new King(true, Color.White, new Cell(1, 'E'), 1));
        // pieces.Add(new King(true, Color.Black, new Cell(8, 'E'), 1));
    }
}