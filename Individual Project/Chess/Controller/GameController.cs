using System.Xml.Schema;
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
        if (selectPiece == null){
            System.Console.WriteLine("Tidak ada bidak di posisi tersebut");
            return false;
        }
        if (selectPiece.GetColor() != players[0].GetColor()){
            System.Console.WriteLine("Bidak tersebut bukan milik pemain ini");
            return false;
        }
        return true;
    }

    public bool ValidateDestination(IPiece piece, Cell destinationCell) {
        IPiece pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
        if (pieceAtDest != null && pieceAtDest.GetColor() == piece.GetColor()) {
            System.Console.WriteLine();
            return false;
        }
        return true;
    }

    public bool ValidateMove(IPiece piece, Cell destinationCell) {
        List<Cell> possibleMoves = piece.GetMovePattern();
        if (!possibleMoves.Contains(destinationCell)) {
            System.Console.WriteLine("Gerakan bidak tidak valid.");
            return false;
        }
        return true;
    }

    // public bool ValidateChecked()
    // {

    // }
    // public bool ValidateCheckmate()
    // {

    // }

    

    public void PieceMove(IPiece pieceToMove, Cell destinationCell)
    {
        IPiece pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
        if (pieceAtDest != null && pieceAtDest.GetColor() == pieceToMove.GetColor()){
            pieceAtDest.SetIsAlive(false);
            System.Console.WriteLine($"{pieceAtDest.GetPieceType()} milik {pieceAtDest.GetColor()} ditangkap!");
        }

        pieceToMove.SetPosition(destinationCell);

        if (pieceToMove is Pawn pawn) {
            pawn.isFirstMove = false;
            if ((pawn.GetColor() == Color.White && destinationCell.row == 7) || 
                (pawn.GetColor() == Color.Black && destinationCell.row == 0)) {
                    PromotePawn(pawn);
            }
        }
        if (pieceToMove is King king) king.isCanCastling = false;

        board.SetBoard(pieces);
    }

    public void PlayerMove(Cell fromCell, Cell toCell) {
        if (!ValidatePiece(fromCell)) {
            System.Console.WriteLine("Pilihan bidak tidak valid.");
            return;
        }

        IPiece pieceToMove = pieces.FirstOrDefault(p => p.GetPosition().Equals(fromCell) && p.GetIsAlive());
        if (pieceToMove == null) {
            System.Console.WriteLine("Bidak tidak ditemukan.");
            return;
        }

        if (!ValidateDestination(pieceToMove, toCell) || !ValidateMove(pieceToMove, toCell)) {
            System.Console.WriteLine("Langkah tidak valid.");
            return;
        }

        PieceMove(pieceToMove, toCell);
        System.Console.WriteLine("Langkah berhasil.");

    }

    public IPiece PromotePawn(Pawn pawn)
    {
        Queen newQueen = new Queen(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
        if (this.pieces != null) {
            int index = this.pieces.IndexOf(pawn);
            if (index != -1) {
                this.pieces[index] = newQueen;
            } else {
                this.pieces.Remove(pawn);
                this.pieces.Add(newQueen);
            }
        }
        pawn.SetIsAlive(false);
        System.Console.WriteLine($"Pion di {newQueen.GetPosition()} dipromosikan menjadi Ratu {newQueen.GetColor()}");
        return newQueen;
    }

    public void EnPassantMove()
    {

    }

    public void CastlingKing()
    {

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