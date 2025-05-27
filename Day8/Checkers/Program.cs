using System;
using System.Collections.Generic;

namespace CheckersGame
{
    // Enum untuk jenis piece
    public enum PieceType
    {
        None,
        Normal,
        King
    }

    // Enum untuk pemain
    public enum Player
    {
        None,
        White,
        Black
    }

    // Class untuk representasi piece (bidak)
    public class Piece
    {
        public PieceType Type { get; set; }
        public Player Color { get; private set; }

        public Piece(Player color)
        {
            Color = color;
            Type = PieceType.Normal;
        }

        public void PromoteToKing()
        {
            Type = PieceType.King;
        }

        public override string ToString()
        {
            if (Color == Player.White)
                return Type == PieceType.King ? "WK" : "W ";
            else if (Color == Player.Black)
                return Type == PieceType.King ? "BK" : "B ";
            return "  ";
        }
    }

    // Class untuk representasi posisi di board
    public struct Position
    {
        public int Row { get; }
        public int Col { get; }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }

    // Class untuk representasi pergerakan
        public class Move
        {
            public Position From { get; }
            public Position To { get; }
            public List<Position> CapturedPositions { get; }

            public Move(Position from, Position to)
            {
                From = from;
                To = to;
                CapturedPositions = new List<Position>();
            }

            public Move(Position from, Position to, List<Position> captured)
            {
                From = from;
                To = to;
                CapturedPositions = captured;
            }
        }

    // Class utama untuk game checkers
    public class CheckersGame
    {
        private Piece[,] board;
        public Player CurrentPlayer { get; private set; }
        public bool GameOver { get; private set; }

        public CheckersGame()
        {
            board = new Piece[8, 8];
            CurrentPlayer = Player.White;
            GameOver = false;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Inisialisasi piece untuk player Black (atas)
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        board[row, col] = new Piece(Player.Black);
                    }
                }
            }

            // Inisialisasi piece untuk player White (bawah)
            for (int row = 5; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        board[row, col] = new Piece(Player.White);
                    }
                }
            }
        }

        public Piece GetPiece(Position pos)
        {
            if (IsValidPosition(pos))
                return board[pos.Row, pos.Col];
            return null;
        }

        private bool IsValidPosition(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Col >= 0 && pos.Col < 8;
        }

        public bool MakeMove(Move move)
        {
            if (GameOver || !IsValidMove(move))
                return false;

            // Eksekusi pergerakan
            Piece piece = board[move.From.Row, move.From.Col];
            board[move.From.Row, move.From.Col] = null;
            board[move.To.Row, move.To.Col] = piece;

            // Tangkap piece lawan jika ada
            foreach (var capturedPos in move.CapturedPositions)
            {
                board[capturedPos.Row, capturedPos.Col] = null;
            }

            // Promosi menjadi king jika mencapai ujung board
            if ((piece.Color == Player.White && move.To.Row == 0) || 
                (piece.Color == Player.Black && move.To.Row == 7))
            {
                piece.PromoteToKing();
            }

            // Ganti giliran pemain
            CurrentPlayer = (CurrentPlayer == Player.White) ? Player.Black : Player.White;

            // Cek jika game sudah berakhir
            CheckGameOver();

            return true;
        }

        private bool IsValidMove(Move move)
        {
            // Validasi dasar
            if (!IsValidPosition(move.From) || !IsValidPosition(move.To))
                return false;

            Piece piece = GetPiece(move.From);
            if (piece == null || piece.Color != CurrentPlayer)
                return false;

            if (GetPiece(move.To) != null)
                return false;

            // Validasi pergerakan berdasarkan jenis piece
            int rowDiff = move.To.Row - move.From.Row;
            int colDiff = Math.Abs(move.To.Col - move.From.Col);

            if (piece.Type == PieceType.Normal)
            {
                int direction = (piece.Color == Player.White) ? -1 : 1;
                
                // Pergerakan normal (tidak menangkap)
                if (colDiff == 1 && rowDiff == direction)
                    return true;
                
                // Pergerakan menangkap
                if (colDiff == 2 && rowDiff == 2 * direction)
                {
                    Position middle = new Position(
                        (move.From.Row + move.To.Row) / 2,
                        (move.From.Col + move.To.Col) / 2);
                    Piece middlePiece = GetPiece(middle);
                    return middlePiece != null && middlePiece.Color != CurrentPlayer;
                }
            }
            else if (piece.Type == PieceType.King)
            {
                // King bisa bergerak diagonal ke segala arah
                if (Math.Abs(rowDiff) == Math.Abs(colDiff))
                {
                    // Cek jika ada piece di jalur
                    int rowStep = rowDiff > 0 ? 1 : -1;
                    int colStep = colDiff > 0 ? 1 : -1;
                    int capturedCount = 0;
                    Position? capturedPos = null;

                    for (int i = 1; i < Math.Abs(rowDiff); i++)
                    {
                        Position current = new Position(
                            move.From.Row + i * rowStep,
                            move.From.Col + i * colStep);
                        Piece currentPiece = GetPiece(current);

                        if (currentPiece != null)
                        {
                            if (currentPiece.Color == CurrentPlayer || capturedCount > 0)
                                return false;
                            capturedCount++;
                            capturedPos = current;
                        }
                    }

                    if (capturedCount == 1)
                    {
                        move.CapturedPositions.Add((Position)capturedPos);
                        return true;
                    }
                    else if (capturedCount == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void CheckGameOver()
        {
            bool whiteHasPieces = false;
            bool blackHasPieces = false;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = board[row, col];
                    if (piece != null)
                    {
                        if (piece.Color == Player.White)
                            whiteHasPieces = true;
                        else if (piece.Color == Player.Black)
                            blackHasPieces = true;
                    }
                }
            }

            if (!whiteHasPieces || !blackHasPieces)
            {
                GameOver = true;
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7");
            for (int row = 0; row < 8; row++)
            {
                Console.Write(row + " ");
                for (int col = 0; col < 8; col++)
                {
                    Piece piece = board[row, col];
                    Console.Write(piece != null ? piece.ToString() : "  ");
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Current Player: {CurrentPlayer}");
        }
    }

    // Program utama
    class Program
    {
        static void Main(string[] args)
        {
            CheckersGame game = new CheckersGame();
            
            while (!game.GameOver)
            {
                game.PrintBoard();
                
                Console.WriteLine("Enter your move (fromRow fromCol toRow toCol):");
                string[] input = Console.ReadLine().Split(' ');
                
                if (input.Length == 4 &&
                    int.TryParse(input[0], out int fromRow) &&
                    int.TryParse(input[1], out int fromCol) &&
                    int.TryParse(input[2], out int toRow) &&
                    int.TryParse(input[3], out int toCol))
                {
                    Move move = new Move(
                        new Position(fromRow, fromCol),
                        new Position(toRow, toCol));
                    
                    if (!game.MakeMove(move))
                    {
                        Console.WriteLine("Invalid move! Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter 4 numbers separated by spaces.");
                }
            }
            
            game.PrintBoard();
            Console.WriteLine("Game Over!");
        }
    }
}