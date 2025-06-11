using Chess;

public class GameController
{
    public Status status;
    public List<IPlayer> players;
    public List<IPiece> pieces;
    public IBoard board;
    public View view;
    Action<List<ICell>, List<IPiece>>? scanCheckmate;
    public GameController(List<IPlayer> player, List<IPiece> pieces, IBoard board, View view)
    {
        players = player;
        this.pieces = pieces;
        this.board = board;
        this.view = view;
        status = Status.Normal;
    }
    public bool ValidatePiece(ICell pieceCell)
    {
        IPiece? selectPiece = pieces.FirstOrDefault(p => p.GetPosition().Equals(pieceCell) && p.GetIsAlive());
        if (selectPiece == null)
        {
            view.ShowNoPieceAtPosition();
            return false;
        }
        if (selectPiece.GetColor() != players[0].GetColor())
        {
            view.ShowNotYourPiece();
            return false;
        }
        return true;
    }

    public bool IsValidDestination(IPiece piece, ICell destinationCell)
    {
        // Checks if the destination cell is either empty or occupied by an opponent's piece.
        IPiece? pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
        if (pieceAtDest != null && pieceAtDest.GetColor() == piece.GetColor())
        {
            return false;
        }
        return true;
    }

    public bool IsMoveLegal(IPiece piece, ICell destinationCell)
    {
        List<ICell> possibleMoves = piece.GetMovePattern();

        // Check if king is eligible for castling
        if (piece is King king && king.isCanCastling)
        {
            int row = king.GetColor() == Color.White ? 1 : 8;
            if ((destinationCell.column == 'G' || destinationCell.column == 'C') && destinationCell.row == row)
            {
                return true;
            }
        }

        if (!possibleMoves.Contains(destinationCell))
        {
            view.ShowInvalidPieceMove();
            return false;
        }

        // Logic for Pawn movement
        if (piece is Pawn pawn)
        {
            ICell currentPos = pawn.GetPosition();
            int direction = pawn.GetColor() == Color.White ? 1 : -1;
            IPiece? pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());

            // 1 square forward
            if (destinationCell.row == currentPos.row + direction && destinationCell.column == currentPos.column)
            {
                if (pieceAtDest != null) return false; // Dihalangi
            }
            // 2 square forward (first move only)
            else if (pawn.isFirstMove && destinationCell.row == currentPos.row + (2 * direction) && destinationCell.column == currentPos.column)
            {
                ICell pathCell = new Cell(currentPos.row + direction, currentPos.column);
                if (pieceAtDest != null || pieces.Any(p => p.GetPosition().Equals(pathCell) && p.GetIsAlive())) return false; // Dihalangi
            }
            // Diagonal capture
            else if (destinationCell.row == currentPos.row + direction && Math.Abs(destinationCell.column - currentPos.column) == 1)
            {
                // En passant
                if (pieceAtDest == null)
                {
                    ICell adjacentCell = new Cell(currentPos.row, destinationCell.column);
                    IPiece? enPassantTarget = pieces.FirstOrDefault(p => p.GetPosition().Equals(adjacentCell) && 
                        p.GetIsAlive() && p is Pawn && p.GetColor() != pawn.GetColor());
                    if (enPassantTarget == null || !((Pawn)enPassantTarget).isCanEnPassant) return false; // Bukan en passant valid
                }
            }
            else
            {
                return false;
            }
        }

        // Simulate the move to check if the King will be in check
        IPiece? originalPieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
        ICell originalPosition = piece.GetPosition();

        piece.SetPosition(destinationCell);
        if (originalPieceAtDest != null)
        {
            originalPieceAtDest.SetIsAlive(false);
        }

        bool isKingInCheck = ValidateChecked(piece.GetColor());

        piece.SetPosition(originalPosition);
        if (originalPieceAtDest != null)
        {
            originalPieceAtDest.SetIsAlive(true);
        }

        if (isKingInCheck)
        {
            view.ShowKingInCheck();
            return false;
        }

        return true;
    }


    public bool ValidateChecked(Color color)
    {
        IPiece? king = pieces.FirstOrDefault(p => p is King && p.GetColor() == color && p.GetIsAlive());
        if (king == null) return false;

        // Check if the king is under attack by any opponent piece
        foreach (var opponentPiece in pieces.Where(p => p.GetColor() != color && p.GetIsAlive()))
        {
            List<ICell> opponentMoves = opponentPiece.GetMovePattern();

            // Exception for pawns: their movement pattern differs from their attack pattern
            if (opponentPiece is Pawn pawn)
            {
                int direction = pawn.GetColor() == Color.White ? 1 : -1;
                ICell pos = pawn.GetPosition();

                if ((king.GetPosition().row == pos.row + direction && king.GetPosition().column == pos.column + 1) ||
                    (king.GetPosition().row == pos.row + direction && king.GetPosition().column == pos.column - 1))
                {
                    return true;
                }
                continue;
            }

            // Check if any opponent piece can attack the king
            if (opponentMoves.Contains(king.GetPosition()))
            {
                if (opponentPiece is Rook || opponentPiece is Bishop || opponentPiece is Queen)
                {
                    int rowStep = Math.Sign(king.GetPosition().row - opponentPiece.GetPosition().row);
                    int colStep = Math.Sign(king.GetPosition().column - opponentPiece.GetPosition().column);
                    ICell currentPathCell = new Cell(opponentPiece.GetPosition().row + rowStep, (char)(opponentPiece.GetPosition().column + colStep));
                    bool pathIsClear = true;
                    while (!currentPathCell.Equals(king.GetPosition()))
                    {
                        if (pieces.Any(p => p.GetPosition().Equals(currentPathCell) && p.GetIsAlive()))
                        {
                            pathIsClear = false;
                            break;
                        }
                        currentPathCell = new Cell(currentPathCell.row + rowStep, (char)(currentPathCell.column + colStep));
                    }
                    if (pathIsClear) return true;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool ValidateCheckmate()
    {
        Color currentPlayerColor = players[0].GetColor();
    
        if (!ValidateChecked(currentPlayerColor))
        {
            return false;
        }
    
        List<ICell> possibleMoves = new List<ICell>();
        List<IPiece> involvedPieces = new List<IPiece>();
    
        // Iterate through all pieces belonging to the current player.
        foreach (var piece in pieces.Where(p => p.GetColor() == currentPlayerColor && p.GetIsAlive()))
        {
            for (char c = 'A'; c <= 'H'; c++)
            {
                for (int r = 1; r <= 8; r++)
                {
                    ICell destinationCell = new Cell(r, c);
    
                    if (!IsValidDestination(piece, destinationCell)) continue;
    
                    // Perform full move validation (including obstacle checks and special rules)
                    bool isMoveTheoreticallyPossible = false;
    
                    // Simplified move validation logic (without check simulation)
                    if (piece is Pawn pawn)
                    {
                        int direction = pawn.GetColor() == Color.White ? 1 : -1;
                        IPiece? pieceAtDestCheck = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
                        if ((destinationCell.row == pawn.GetPosition().row + direction && destinationCell.column == pawn.GetPosition().column && pieceAtDestCheck == null) ||
                            (pawn.isFirstMove && destinationCell.row == pawn.GetPosition().row + (2 * direction) && destinationCell.column == pawn.GetPosition().column && pieceAtDestCheck == null) ||
                            (destinationCell.row == pawn.GetPosition().row + direction && Math.Abs(destinationCell.column - pawn.GetPosition().column) == 1 && pieceAtDestCheck != null))
                            isMoveTheoreticallyPossible = true;
                    }
                    else if (piece is Rook || piece is Bishop || piece is Queen)
                    {
                        if (piece.GetMovePattern().Contains(destinationCell))
                        {
                            // Check if the path is clear for Rook, Bishop, or Queen
                            int rowStep = Math.Sign(destinationCell.row - piece.GetPosition().row);
                            int colStep = Math.Sign(destinationCell.column - piece.GetPosition().column);
                            ICell path = new Cell(piece.GetPosition().row + rowStep, (char)(piece.GetPosition().column + colStep));
                            bool blocked = false;
                            while (!path.Equals(destinationCell))
                            {
                                if (pieces.Any(p => p.GetPosition().Equals(path) && p.GetIsAlive())) { blocked = true; break; }
                                path = new Cell(path.row + rowStep, (char)(path.column + colStep));
                            }
                            if (!blocked) isMoveTheoreticallyPossible = true;
                        }
                    }
                    else
                    {
                        if (piece.GetMovePattern().Contains(destinationCell)) isMoveTheoreticallyPossible = true;
                    }
    
                    if (!isMoveTheoreticallyPossible) continue;
    
                    // Add the move and piece to the tracking lists
                    possibleMoves.Add(destinationCell);
                    involvedPieces.Add(piece);
    
                    ICell originalPosition = piece.GetPosition();
                    IPiece? pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
                    bool destPieceWasAlive = pieceAtDest?.GetIsAlive() ?? false;
    
                    piece.SetPosition(destinationCell);
                    if (pieceAtDest != null) pieceAtDest.SetIsAlive(false);
    
                    bool stillInCheck = ValidateChecked(currentPlayerColor);
    
                    piece.SetPosition(originalPosition);
                    if (pieceAtDest != null) pieceAtDest.SetIsAlive(destPieceWasAlive);
    
                    if (!stillInCheck)
                    {
                        return false;
                    }
                }
            }
        }
    
        // Invoke the ScanCheckmate action if it's not null
        scanCheckmate?.Invoke(possibleMoves, involvedPieces);
    
        return true;
    }

    public void PieceMove(IPiece pieceToMove, ICell destinationCell)
    {

        if (pieceToMove is King king)
        {
            if (CastlingKing(king, destinationCell))
            {
                return;
            }
        }

        if (pieceToMove is Pawn pawn)
        {
            // First check for en passant move
            if (EnPassantMove(pawn, destinationCell))
            {
                board.SetBoard(pieces);
                return;
            }

            // Reset en passant eligibility for all pawns of same color
            foreach (var p in pieces.OfType<Pawn>().Where(p => p.GetColor() == pawn.GetColor()))
            {
                p.isCanEnPassant = false;
            }

            // Check if this is a two-square move that makes this pawn eligible for en passant
            if (Math.Abs(destinationCell.row - pawn.GetPosition().row) == 2)
            {
                pawn.isCanEnPassant = true;
            }

            pawn.isFirstMove = false;
        }

        IPiece? pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
        if (pieceAtDest != null && pieceAtDest.GetColor() != pieceToMove.GetColor())
        {
            pieceAtDest.SetIsAlive(false);
            view.ShowCapturedPiece(pieceAtDest.GetPieceType().ToString(), pieceAtDest.GetColor());
        }

        pieceToMove.SetPosition(destinationCell);

        if (pieceToMove is Pawn promotedPawn)
        {
            promotedPawn.isFirstMove = false;
            if ((promotedPawn.GetColor() == Color.White && destinationCell.row == 8) ||
                (promotedPawn.GetColor() == Color.Black && destinationCell.row == 1))
            {
                PromotePawn(promotedPawn);
            }
        }
        if (pieceToMove is King k) k.isCanCastling = false;

        board.SetBoard(pieces);
    }

    public void PlayerMove(ICell fromCell, ICell toCell)
    {
        if (!ValidatePiece(fromCell))
        {
            return;
        }

        IPiece pieceToMove = pieces.First(p => p.GetPosition().Equals(fromCell) && p.GetIsAlive());

        if (!IsValidDestination(pieceToMove, toCell) || !IsMoveLegal(pieceToMove, toCell))
        {
            return;
        }

        PieceMove(pieceToMove, toCell);
        view.ShowMoveSuccess();

        // After moving, check the opponent's status
        IPlayer opponentPlayer = players[1];
        if (ValidateChecked(opponentPlayer.GetColor()))
        {
            IPlayer currentPlayer = players[0];
            players[0] = opponentPlayer;
            players[1] = currentPlayer;

            if (ValidateCheckmate())
            {
                status = Status.Checkmate;
                opponentPlayer.SetStatus(Status.Checkmate);
                view.ShowCheckmate(currentPlayer.GetColor());
            }
            else
            {
                status = Status.Check;
                opponentPlayer.SetStatus(Status.Check);
                view.ShowCheck(currentPlayer.GetColor());
            }

            players[0] = currentPlayer;
            players[1] = opponentPlayer;
        }
        else
        {
            status = Status.Normal;
            opponentPlayer.SetStatus(Status.Normal);
        }

        // Switch player turn
        if (players.Count > 1)
        {
            IPlayer movePlayer = players[0];
            players.RemoveAt(0);
            players.Add(movePlayer);
        }
    }

    public IPiece PromotePawn(Pawn pawn)
    {
        view.ShowPawnPromotionChoice();
        string? choice = Console.ReadLine()?.ToUpper();
        IPiece promotedPiece;
        switch (choice)
        {
            case "R":
                promotedPiece = new Rook(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                view.ShowPawnPromoted(promotedPiece.GetPieceType().ToString(), promotedPiece.GetPosition());
                break;
            case "B":
                promotedPiece = new Bishop(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                view.ShowPawnPromoted(promotedPiece.GetPieceType().ToString(), promotedPiece.GetPosition());
                break;
            case "N":
                promotedPiece = new Knight(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                view.ShowPawnPromoted(promotedPiece.GetPieceType().ToString(), promotedPiece.GetPosition());
                break;
            default:
                promotedPiece = new Queen(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                view.ShowPawnPromoted(promotedPiece.GetPieceType().ToString(), promotedPiece.GetPosition());
                break;
        }

        pawn.SetIsAlive(false);
        pieces.Remove(pawn); 
        pieces.Add(promotedPiece);

        return promotedPiece;
    }

    public bool EnPassantMove(Pawn pawn, ICell destination)
    {
        int direction = pawn.GetColor() == Color.White ? 1 : -1;
        if (destination.row != pawn.GetPosition().row + direction || Math.Abs(destination.column - pawn.GetPosition().column) != 1) return false;
        if (pieces.Any(p => p.GetPosition().Equals(destination) && p.GetIsAlive())) return false;

        // The captured pawn must be adjacent and eligible for en passant
        ICell capturedPawnCell = new Cell(pawn.GetPosition().row, destination.column);
        IPiece? capturedPawn = pieces.FirstOrDefault(p => p.GetPosition().Equals(capturedPawnCell) && p is Pawn && ((Pawn)p).isCanEnPassant && p.GetColor() != pawn.GetColor());

        if (capturedPawn == null) return false;

        // Perform en passant
        capturedPawn.SetIsAlive(false);
        pawn.SetPosition(destination);
        pawn.isFirstMove = false;
        view.ShowEnPassantSuccess();
        return true;
    }

    public bool CastlingKing(King king, ICell destination)
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
            Rook? rook = pieces.OfType<Rook>().FirstOrDefault(r =>
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

    public void SetupInitialPieces(List<IPiece> pieces)
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

    public void StartGame()
    {
        status = Status.Normal;
        foreach (var player in players)
        {
            board.SetBoard(pieces);
            player.SetStatus(Status.Normal);
        }
    }

    public bool EndGame()
    {
        return status == Status.Checkmate;
    }
}