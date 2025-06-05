using System.Drawing;
using Chess;

public class GameController
{
    public Status status { get; set; }
    public List<IPlayer> players { get; set; }
    public List<IPiece> pieces { get; set; }
    public IBoard board { get; set; }
    Action<List<Cell>, List<IPiece>>? ScanCheckmate;
    public GameController(List<IPlayer> player, List<IPiece> pieces, IBoard board)
    {
        players = player;
        this.pieces = pieces;
        this.board = board;
        status = Status.Normal;
    }
    public bool ValidatePiece(Cell pieceCell)
    {
        IPiece selectPiece = pieces.FirstOrDefault(p => p.GetPosition().Equals(pieceCell) && p.GetIsAlive());
        if (selectPiece == null)
        {
            System.Console.WriteLine("Tidak ada bidak di posisi tersebut");
            return false;
        }
        if (selectPiece.GetColor() != players[0].GetColor())
        {
            System.Console.WriteLine("Bidak tersebut bukan milik pemain ini");
            return false;
        }
        return true;
    }

    public bool ValidateDestination(IPiece piece, Cell destinationCell)
    {
        IPiece pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
        if (pieceAtDest != null && pieceAtDest.GetColor() == piece.GetColor())
        {
            System.Console.WriteLine();
            return false;
        }
        return true;
    }

    public bool ValidateMove(IPiece piece, Cell destinationCell)
    {
        List<Cell> possibleMoves = piece.GetMovePattern();

        // Allow castling move for king
        if (piece is King king && king.isCanCastling)
        {
            int row = king.GetColor() == Color.White ? 1 : 8;
            if ((destinationCell.column == 'G' || destinationCell.column == 'C') && destinationCell.row == row)
            {
                return true;
            }
        }

        // Check if the destination cell is within the possible moves
        if (!possibleMoves.Contains(destinationCell))
        {
            System.Console.WriteLine("Invalid piece move.");
            return false;
        }

        // Additional condition for pawn diagonal movement
        if (piece is Pawn pawn)
        {
            int rowDiff = Math.Abs(destinationCell.row - piece.GetPosition().row);
            int colDiff = Math.Abs(destinationCell.column - piece.GetPosition().column);

            // If the pawn moves diagonally, ensure it is capturing an opponent piece
            if (rowDiff == 1 && colDiff == 1)
            {
                IPiece pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
                if (pieceAtDest == null || pieceAtDest.GetColor() == piece.GetColor())
                {
                    System.Console.WriteLine("Pion tidak dapat bergerak secara diagonal kecuali menangkap bidak lawan.");
                    return false;
                }
            }
        }

        return true;
    }

    public bool ValidateChecked(Color color)
    {
        IPiece king = pieces.FirstOrDefault(p => p.GetPieceType() == PieceEnum.King && p.GetColor() == color && p.GetIsAlive());
        if (king == null) return false;

        return false;
    }



    // public bool ValidateCheckmate()
    // {

    // }



    public void PieceMove(IPiece pieceToMove, Cell destinationCell)
    {
        if (pieceToMove is King king)
        {
            if (CastlingKing(king, destinationCell))
            {
                return;
            }
        }

        IPiece pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
        if (pieceAtDest != null && pieceAtDest.GetColor() == pieceToMove.GetColor())
        {
            pieceAtDest.SetIsAlive(false);
            System.Console.WriteLine($"{pieceAtDest.GetPieceType()} milik {pieceAtDest.GetColor()} ditangkap!");
        }

        CaptureOpponentPiece(destinationCell, pieceToMove.GetColor());

        pieceToMove.SetPosition(destinationCell);

        if (pieceToMove is Pawn pawn)
        {
            pawn.isFirstMove = false;
            if ((pawn.GetColor() == Color.White && destinationCell.row == 8) ||
                (pawn.GetColor() == Color.Black && destinationCell.row == 1))
            {
                PromotePawn(pawn);
            }
        }
        if (pieceToMove is King k) k.isCanCastling = false;

        board.SetBoard(pieces);
    }

    public void PlayerMove(Cell fromCell, Cell toCell)
    {
        if (!ValidatePiece(fromCell))
        {
            System.Console.WriteLine("Pilihan bidak tidak valid.");
            return;
        }

        IPiece pieceToMove = pieces.FirstOrDefault(p => p.GetPosition().Equals(fromCell) && p.GetIsAlive());
        if (pieceToMove == null)
        {
            return;
        }

        if (!ValidateDestination(pieceToMove, toCell) || !ValidateMove(pieceToMove, toCell))
        {
            return;
        }

        PieceMove(pieceToMove, toCell);
        System.Console.WriteLine("Langkah berhasil.");

        if (ValidateChecked(players[0].GetColor()))
        {
            status = Status.Check;
            System.Console.WriteLine($"{players[0].GetColor()} is in check!");
        }
        else
        {
            status = Status.Normal;
        }

        if (players.Count > 1)
        {
            IPlayer movePlayer = players[0];
            players.RemoveAt(0);
            players.Add(movePlayer);
        }
    }

    public void CaptureOpponentPiece(Cell destinationCell, Color pieceColor)
    {
        IPiece opponentPiece = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive() && p.GetColor() != pieceColor);
        if (opponentPiece != null)
        {
            opponentPiece.SetIsAlive(false);
            System.Console.WriteLine($"{opponentPiece.GetPieceType()} milik {opponentPiece.GetColor()} ditangkap!");
        }
    }

    public IPiece PromotePawn(Pawn pawn)
    {
        System.Console.WriteLine("Choose a piece to promote the pawn  (Q for Queen, R for Rook, B for Bishop, N for Knight): ");
        string choice = Console.ReadLine()?.ToUpper();
        IPiece promotedPiece;
        switch (choice)
        {
            case "R":
                promotedPiece = new Rook(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                System.Console.WriteLine($"Pawn promoted to Rook at {pawn.GetPosition().column}{pawn.GetPosition().row}");
                break;
            case "B":
                promotedPiece = new Bishop(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                System.Console.WriteLine($"Pawn promoted to Bishop at {pawn.GetPosition().column}{pawn.GetPosition().row}");
                break;
            case "N":
                promotedPiece = new Knight(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                System.Console.WriteLine($"Pawn promoted to Knight at {pawn.GetPosition().column}{pawn.GetPosition().row}");
                break;
            default:
                promotedPiece = new Queen(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                System.Console.WriteLine($"Pawn promoted to Queen at {pawn.GetPosition().column}{pawn.GetPosition().row}");
                break;
        }

        if (this.pieces != null)
        {
            int index = this.pieces.IndexOf(pawn);
            if (index >= 0)
            {
                this.pieces[index] = promotedPiece;
            }
            else
            {
                this.pieces.Remove(pawn);
                this.pieces.Add(promotedPiece);
            }
        }
        pawn.SetIsAlive(false);
        return promotedPiece;
    }

    public void EnPassantMove(Pawn pawn)
    {
        Cell pawnPos = pawn.GetPosition();

        int direction = pawn.GetColor() == Color.White ? 1 : -1;

        foreach (int colOffset in new int[] { -1, 1 })
        {
            char targetCol = (char)(pawnPos.column + colOffset);
            int targetRow = pawnPos.row;

            IPiece? enemyPawn = pieces.FirstOrDefault(p =>
                p is Pawn &&
                p.GetColor() != pawn.GetColor() &&
                p.GetPosition().column == targetCol &&
                p.GetPosition().row == targetRow &&
                ((Pawn)p).isCanEnPassant
            );

            if (enemyPawn != null)
            {
                Cell enPassantDest = new Cell(pawnPos.row + direction, targetCol);

                enemyPawn.SetIsAlive(false);

                pawn.SetPosition(enPassantDest);

                System.Console.WriteLine($"En passant capture at {enPassantDest.column}{enPassantDest.row}!");

                board.SetBoard(pieces);
                return;
            }
        }
    }


    public bool CastlingKing(King king, Cell destination) // Change return to boolean and add parameter
    {
        if (king == null || !king.isCanCastling)
        {
            return false;
        }

        int row = king.GetColor() == Color.White ? 1 : 8;

        // King-side castling
        if (destination.column == 'G' && destination.row == row)
        {
            Rook? rook = pieces.OfType<Rook>().FirstOrDefault(r =>
                r.GetColor() == king.GetColor() && r.GetPosition().Equals(new Cell(row, 'H')) &&
                r.GetIsAlive());

            if (rook == null) return false;

            if (pieces.Any(p => p.GetIsAlive() && p.GetPosition().row == row &&
                p.GetPosition().column == 'F' || p.GetPosition().column == 'G'))
                return false;

            king.SetPosition(new Cell(row, 'G'));
            rook.SetPosition(new Cell(row, 'F'));
            king.isCanCastling = false;
            board.SetBoard(pieces);
            return true;
        }

        // Queen-side castling
        if (destination.column == 'C' && destination.row == row)
        {
            Rook rook = pieces.OfType<Rook>().FirstOrDefault(r =>
                r.GetColor() == king.GetColor() && r.GetPosition().Equals(new Cell(row, 'A')) &&
                r.GetIsAlive());

            if (rook == null) return false;

            if (pieces.Any(p => p.GetIsAlive() && p.GetPosition().row == row &&
                p.GetPosition().column == 'B' || p.GetPosition().column == 'D'))
                return false;

            king.SetPosition(new Cell(row, 'C'));
            rook.SetPosition(new Cell(row, 'D'));
            king.isCanCastling = false;
            board.SetBoard(pieces);
            return true;
        }

        return false;

    }

    public void StartGame()
    {
        status = Status.Normal;
        foreach (var player in players)
        {
            board.SetBoard(pieces);
            player.SetStatus(Status.Normal);
        }
    }

    public void EndGame()
    {

    }
}