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
            // Pesan ini tidak ditampilkan karena validasi ini juga digunakan secara internal oleh ValidateCheckmate
            // System.Console.WriteLine("Tidak bisa memakan bidak sendiri.");
            return false;
        }
        return true;
    }

    public bool ValidateMove(IPiece piece, Cell destinationCell)
    {
        // 1. Validasi Pola Gerakan Dasar & Rintangan
        List<Cell> possibleMoves = piece.GetMovePattern();
        
        // Logika spesifik untuk Pawn
        if (piece is Pawn pawn)
        {
            Cell currentPos = pawn.GetPosition();
            int direction = pawn.GetColor() == Color.White ? 1 : -1;
            IPiece pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());

            // Gerak maju 1 kotak
            if (destinationCell.row == currentPos.row + direction && destinationCell.column == currentPos.column)
            {
                if (pieceAtDest != null) return false; // Dihalangi
            }
            // Gerak maju 2 kotak
            else if (pawn.isFirstMove && destinationCell.row == currentPos.row + (2 * direction) && destinationCell.column == currentPos.column)
            {
                Cell pathCell = new Cell(currentPos.row + direction, currentPos.column);
                if (pieceAtDest != null || pieces.Any(p => p.GetPosition().Equals(pathCell) && p.GetIsAlive())) return false; // Dihalangi
            }
            // Makan diagonal
            else if (destinationCell.row == currentPos.row + direction && Math.Abs(destinationCell.column - currentPos.column) == 1)
            {
                // En passant
                if (pieceAtDest == null)
                {
                    Cell adjacentCell = new Cell(currentPos.row, destinationCell.column);
                    IPiece enPassantTarget = pieces.FirstOrDefault(p => p.GetPosition().Equals(adjacentCell) && p.GetIsAlive() && p is Pawn && p.GetColor() != pawn.GetColor());
                    if (enPassantTarget == null || !((Pawn)enPassantTarget).isCanEnPassant) return false; // Bukan en passant valid
                }
            }
            else
            {
                return false; // Bukan gerakan pion yang valid
            }
        }
        // Logika untuk bidak yang meluncur (Rook, Bishop, Queen)
        else if (piece is Rook || piece is Bishop || piece is Queen)
        {
            if (!possibleMoves.Contains(destinationCell)) return false;

            // Cek rintangan di sepanjang jalur
            int rowStep = Math.Sign(destinationCell.row - piece.GetPosition().row);
            int colStep = Math.Sign(destinationCell.column - piece.GetPosition().column);
            Cell currentPathCell = new Cell(piece.GetPosition().row + rowStep, (char)(piece.GetPosition().column + colStep));

            while (!currentPathCell.Equals(destinationCell))
            {
                if (pieces.Any(p => p.GetPosition().Equals(currentPathCell) && p.GetIsAlive()))
                {
                    return false; // Jalur terhalang
                }
                currentPathCell = new Cell(currentPathCell.row + rowStep, (char)(currentPathCell.column + colStep));
            }
        }
        // Logika untuk King dan Knight (mereka melompat)
        else
        {
             if (!possibleMoves.Contains(destinationCell)) return false;
        }

        // 2. Simulasi langkah untuk memeriksa apakah Raja akan berada dalam kondisi Skak
        // Ini adalah aturan paling fundamental: sebuah langkah tidak valid jika membuat atau membiarkan raja sendiri dalam keadaan skak.
        IPiece originalPieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
        Cell originalPosition = piece.GetPosition();

        // Lakukan simulasi langkah
        piece.SetPosition(destinationCell);
        if (originalPieceAtDest != null)
        {
            originalPieceAtDest.SetIsAlive(false);
        }

        // Periksa apakah raja saat ini dalam kondisi skak
        bool isKingInCheck = ValidateChecked(piece.GetColor());

        // Batalkan simulasi langkah
        piece.SetPosition(originalPosition);
        if (originalPieceAtDest != null)
        {
            originalPieceAtDest.SetIsAlive(true);
        }

        // Jika setelah langkah raja menjadi skak, maka langkah itu tidak valid
        if (isKingInCheck)
        {
            System.Console.WriteLine("Langkah tidak valid: Raja tidak boleh berada dalam kondisi Skak.");
            return false;
        }

        return true;
    }


    public bool ValidateChecked(Color color)
    {
        IPiece king = pieces.FirstOrDefault(p => p is King && p.GetColor() == color && p.GetIsAlive());
        if (king == null) return false;

        // Periksa apakah ada bidak lawan yang bisa menyerang raja
        foreach (var opponentPiece in pieces.Where(p => p.GetColor() != color && p.GetIsAlive()))
        {
            // Dapatkan semua kemungkinan gerakan lawan
            List<Cell> opponentMoves = opponentPiece.GetMovePattern();

            // Pengecualian untuk pion: pola gerakan berbeda dengan pola serangan
            if (opponentPiece is Pawn pawn)
            {
                int direction = pawn.GetColor() == Color.White ? 1 : -1;
                Cell pos = pawn.GetPosition();
                // Pion hanya menyerang secara diagonal
                if ((king.GetPosition().row == pos.row + direction && king.GetPosition().column == pos.column + 1) ||
                    (king.GetPosition().row == pos.row + direction && king.GetPosition().column == pos.column - 1))
                {
                    return true;
                }
                continue; // Lanjut ke bidak berikutnya
            }

            if (opponentMoves.Contains(king.GetPosition()))
            {
                // Untuk bidak yang meluncur, pastikan tidak ada yang menghalangi
                if (opponentPiece is Rook || opponentPiece is Bishop || opponentPiece is Queen)
                {
                     int rowStep = Math.Sign(king.GetPosition().row - opponentPiece.GetPosition().row);
                     int colStep = Math.Sign(king.GetPosition().column - opponentPiece.GetPosition().column);
                     Cell currentPathCell = new Cell(opponentPiece.GetPosition().row + rowStep, (char)(opponentPiece.GetPosition().column + colStep));
                     bool pathIsClear = true;
                     while(!currentPathCell.Equals(king.GetPosition()))
                     {
                         if(pieces.Any(p=>p.GetPosition().Equals(currentPathCell) && p.GetIsAlive()))
                         {
                             pathIsClear = false;
                             break;
                         }
                         currentPathCell = new Cell(currentPathCell.row + rowStep, (char)(currentPathCell.column + colStep));
                     }
                     if(pathIsClear) return true; // Skak jika jalur bersih
                }
                else // Untuk King dan Knight, mereka bisa melompat
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

        // 1. Pastikan raja saat ini sedang dalam kondisi Skak. Jika tidak, itu bukan Skakmat.
        if (!ValidateChecked(currentPlayerColor))
        {
            return false;
        }

        // 2. Iterasi melalui semua bidak milik pemain saat ini.
        foreach (var piece in pieces.Where(p => p.GetColor() == currentPlayerColor && p.GetIsAlive()))
        {
            // 3. Untuk setiap bidak, iterasi melalui setiap kotak di papan sebagai tujuan potensial.
            for (char c = 'A'; c <= 'H'; c++)
            {
                for (int r = 1; r <= 8; r++)
                {
                    Cell destinationCell = new Cell(r, c);

                    // Lakukan validasi awal yang cepat: apakah tujuan valid dan polanya benar.
                    if (!ValidateDestination(piece, destinationCell)) continue;

                    // Lakukan validasi gerakan penuh (yang mencakup pengecekan rintangan dan aturan khusus)
                    // Tapi kita tidak bisa memanggil ValidateMove secara langsung karena akan menimbulkan pesan di konsol.
                    // Jadi kita harus mereplikasi logikanya di sini secara "diam-diam".
                    bool isMoveTheoreticallyPossible = false;
                    
                    // Logika validasi gerakan yang disederhanakan (tanpa simulasi skak)
                    // Ini untuk menyaring gerakan yang jelas-jelas tidak mungkin sebelum melakukan simulasi penuh.
                    if (piece is Pawn pawn) {
                        int direction = pawn.GetColor() == Color.White ? 1 : -1;
                        IPiece pieceAtDestCheck = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
                        if ((destinationCell.row == pawn.GetPosition().row + direction && destinationCell.column == pawn.GetPosition().column && pieceAtDestCheck == null) ||
                            (pawn.isFirstMove && destinationCell.row == pawn.GetPosition().row + (2 * direction) && destinationCell.column == pawn.GetPosition().column && pieceAtDestCheck == null) ||
                            (destinationCell.row == pawn.GetPosition().row + direction && Math.Abs(destinationCell.column - pawn.GetPosition().column) == 1 && pieceAtDestCheck != null))
                            isMoveTheoreticallyPossible = true;
                    } else if (piece is Rook || piece is Bishop || piece is Queen) {
                        if (piece.GetMovePattern().Contains(destinationCell)) {
                            // Cek rintangan
                            int rowStep = Math.Sign(destinationCell.row - piece.GetPosition().row);
                            int colStep = Math.Sign(destinationCell.column - piece.GetPosition().column);
                            Cell path = new Cell(piece.GetPosition().row + rowStep, (char)(piece.GetPosition().column + colStep));
                            bool blocked = false;
                            while(!path.Equals(destinationCell)) {
                                if (pieces.Any(p => p.GetPosition().Equals(path) && p.GetIsAlive())) { blocked = true; break; }
                                path = new Cell(path.row + rowStep, (char)(path.column + colStep));
                            }
                            if (!blocked) isMoveTheoreticallyPossible = true;
                        }
                    } else { // King, Knight
                        if (piece.GetMovePattern().Contains(destinationCell)) isMoveTheoreticallyPossible = true;
                    }
                    
                    if (!isMoveTheoreticallyPossible) continue;


                    // 4. Jika langkah memungkinkan, simulasikan untuk melihat apakah itu menyelamatkan Raja.
                    Cell originalPosition = piece.GetPosition();
                    IPiece pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
                    bool destPieceWasAlive = pieceAtDest?.GetIsAlive() ?? false;

                    // Simulasikan
                    piece.SetPosition(destinationCell);
                    if (pieceAtDest != null) pieceAtDest.SetIsAlive(false);

                    // Periksa apakah raja MASIH dalam kondisi skak setelah simulasi
                    bool stillInCheck = ValidateChecked(currentPlayerColor);

                    // Batalkan simulasi
                    piece.SetPosition(originalPosition);
                    if (pieceAtDest != null) pieceAtDest.SetIsAlive(destPieceWasAlive);
                    
                    // 5. Jika kita menemukan satu saja langkah yang membuat raja tidak lagi skak, maka itu bukan skakmat.
                    if (!stillInCheck)
                    {
                        return false;
                    }
                }
            }
        }

        // 6. Jika semua perulangan selesai dan tidak ada satu pun langkah yang bisa menyelamatkan raja, maka itu adalah Skakmat.
        return true;
    }



    public void PieceMove(IPiece pieceToMove, Cell destinationCell)
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

        IPiece pieceAtDest = pieces.FirstOrDefault(p => p.GetPosition().Equals(destinationCell) && p.GetIsAlive());
        if (pieceAtDest != null && pieceAtDest.GetColor() != pieceToMove.GetColor())
        {
            pieceAtDest.SetIsAlive(false);
            System.Console.WriteLine($"{pieceAtDest.GetPieceType()} milik {pieceAtDest.GetColor()} ditangkap!");
        }

        // CaptureOpponentPiece(destinationCell, pieceToMove.GetColor()); // Ini sudah ditangani di atas

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
        if (pieceToMove is Rook r) {
            // Logika untuk menonaktifkan castling jika benteng bergerak bisa ditambahkan di sini
        }

        board.SetBoard(pieces);
    }

    public void PlayerMove(Cell fromCell, Cell toCell)
    {
        if (!ValidatePiece(fromCell))
        {
            // System.Console.WriteLine("Pilihan bidak tidak valid."); // Pesan sudah ada di ValidatePiece
            return;
        }

        IPiece pieceToMove = pieces.First(p => p.GetPosition().Equals(fromCell) && p.GetIsAlive());

        if (!ValidateDestination(pieceToMove, toCell) || !ValidateMove(pieceToMove, toCell))
        {
            // System.Console.WriteLine("Langkah tidak valid."); // Pesan sudah ada di ValidateMove
            return;
        }

        PieceMove(pieceToMove, toCell);
        System.Console.WriteLine("Langkah berhasil.");

        // Setelah bergerak, periksa status lawan
        IPlayer opponentPlayer = players[1];
        if (ValidateChecked(opponentPlayer.GetColor()))
        {
            // Lawan dalam kondisi skak. Periksa apakah itu skakmat.
            // Untuk memeriksa skakmat, kita perlu mengubah konteks pemain sementara
            IPlayer currentPlayer = players[0];
            players[0] = opponentPlayer;
            players[1] = currentPlayer;

            if (ValidateCheckmate())
            {
                status = Status.Checkmate;
                opponentPlayer.SetStatus(Status.Checkmate);
                System.Console.WriteLine($"SKAKMAT! {currentPlayer.GetColor()} menang!");
            }
            else
            {
                status = Status.Check;
                opponentPlayer.SetStatus(Status.Check);
                System.Console.WriteLine($"SKAK! Raja {opponentPlayer.GetColor()} dalam bahaya.");
            }

            // Kembalikan urutan pemain ke semula
            players[0] = currentPlayer;
            players[1] = opponentPlayer;
        }
        else
        {
            status = Status.Normal;
            opponentPlayer.SetStatus(Status.Normal);
        }

        // Ganti giliran pemain
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
        System.Console.WriteLine("Pilih bidak untuk promosi (Q: Ratu, R: Benteng, B: Gajah, N: Kuda): ");
        string choice = Console.ReadLine()?.ToUpper();
        IPiece promotedPiece;
        switch (choice)
        {
            case "R":
                promotedPiece = new Rook(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                System.Console.WriteLine($"Pion dipromosikan menjadi Benteng di {pawn.GetPosition().column}{pawn.GetPosition().row}");
                break;
            case "B":
                promotedPiece = new Bishop(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                System.Console.WriteLine($"Pion dipromosikan menjadi Gajah di {pawn.GetPosition().column}{pawn.GetPosition().row}");
                break;
            case "N":
                promotedPiece = new Knight(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                System.Console.WriteLine($"Pion dipromosikan menjadi Kuda di {pawn.GetPosition().column}{pawn.GetPosition().row}");
                break;
            default:
                promotedPiece = new Queen(true, pawn.GetColor(), pawn.GetPosition(), pawn.GetPieceOrdinal());
                System.Console.WriteLine($"Pion dipromosikan menjadi Ratu di {pawn.GetPosition().column}{pawn.GetPosition().row}");
                break;
        }

        pawn.SetIsAlive(false); // Matikan pion yang lama
        pieces.Remove(pawn);    // Hapus pion dari daftar
        pieces.Add(promotedPiece); // Tambahkan bidak baru hasil promosi
        
        return promotedPiece;
    }

    public bool EnPassantMove(Pawn pawn, Cell destination)
    {
        int direction = pawn.GetColor() == Color.White ? 1 : -1;
        if (destination.row != pawn.GetPosition().row + direction || Math.Abs(destination.column - pawn.GetPosition().column) != 1) return false;
        if (pieces.Any(p => p.GetPosition().Equals(destination) && p.GetIsAlive())) return false;
        
        Cell capturedPawnCell = new Cell(pawn.GetPosition().row, destination.column);
        IPiece capturedPawn = pieces.FirstOrDefault(p => p.GetPosition().Equals(capturedPawnCell) && p is Pawn && ((Pawn)p).isCanEnPassant && p.GetColor() != pawn.GetColor());

        if (capturedPawn == null) return false;

        capturedPawn.SetIsAlive(false);
        pawn.SetPosition(destination);
        pawn.isFirstMove = false;
        System.Console.WriteLine("En Passant berhasil!");
        return true;
    }

    public bool CastlingKing(King king, Cell destination)
    {
        if (king == null || !king.isCanCastling || ValidateChecked(king.GetColor())) return false;

        int row = king.GetColor() == Color.White ? 1 : 8;

        // King-side castling (Rokade pendek)
        if (destination.column == 'G' && destination.row == row)
        {
            // Cek apakah benteng ada di posisi & hidup
            Rook? rook = pieces.OfType<Rook>().FirstOrDefault(r => r.GetColor() == king.GetColor() && r.GetPosition().Equals(new Cell(row, 'H')) && r.GetIsAlive());
            if (rook == null) return false;

            // Cek apakah jalur kosong
            if (pieces.Any(p => p.GetIsAlive() && p.GetPosition().row == row && (p.GetPosition().column == 'F' || p.GetPosition().column == 'G'))) return false;
            
            // Cek apakah jalur yang dilewati raja diserang
            king.SetPosition(new Cell(row, 'F'));
            if (ValidateChecked(king.GetColor())) { king.SetPosition(new Cell(row, 'E')); return false; }

            king.SetPosition(new Cell(row, 'G'));
            if (ValidateChecked(king.GetColor())) { king.SetPosition(new Cell(row, 'E')); return false; }
            
            rook.SetPosition(new Cell(row, 'F'));
            king.isCanCastling = false;
            board.SetBoard(pieces);
            return true;
        }

        // Queen-side castling (Rokade panjang)
        if (destination.column == 'C' && destination.row == row)
        {
            Rook? rook = pieces.OfType<Rook>().FirstOrDefault(r => r.GetColor() == king.GetColor() && r.GetPosition().Equals(new Cell(row, 'A')) && r.GetIsAlive());
            if (rook == null) return false;
            
            if (pieces.Any(p => p.GetIsAlive() && p.GetPosition().row == row && (p.GetPosition().column == 'B' || p.GetPosition().column == 'C' || p.GetPosition().column == 'D'))) return false;

            king.SetPosition(new Cell(row, 'D'));
            if (ValidateChecked(king.GetColor())) { king.SetPosition(new Cell(row, 'E')); return false; }
            
            king.SetPosition(new Cell(row, 'C'));
            if (ValidateChecked(king.GetColor())) { king.SetPosition(new Cell(row, 'E')); return false; }

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

    public bool EndGame()
    {
        // Permainan berakhir jika statusnya Skakmat atau Stalemate
        return status == Status.Checkmate || status == Status.Stalemate;
    }
}