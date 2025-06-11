using Chess;

[TestFixture]
public class ValidatePiece
{
    private GameController _gameController;
    private List<IPlayer> _players;
    private List<IPiece> _pieces;
    private IBoard _board;
    private View _view;

    [SetUp]
    public void Setup()
    {
        // Initialize mock objects for testing
        _players = new List<IPlayer>();
        _pieces = new List<IPiece>();
        _board = new Board(8, 8);
        _view = new View();

        // Create test players
        var whitePlayer = new Player(Color.White, Status.Normal);
        var blackPlayer = new Player(Color.Black, Status.Normal);
        _players.Add(whitePlayer);
        _players.Add(blackPlayer);

        _gameController = new GameController(_players, _pieces, _board, _view);
    }

    [Test]
    public void ValidatePiece_EmptyCellChoice_ReturnsFalse()
    {
        // Arrange
        var whitePawn = new Pawn(true, Color.White, new Cell(2, 'A'), 1);
        var blackPawn = new Pawn(true, Color.Black, new Cell(3, 'A'), 1);
        var Cell = new Cell(4, 'A');

        // Act
        bool result = _gameController.ValidatePiece(Cell);

        // Assert
        Assert.That(result, Is.False, "Should return false when piece choice is empty");
    }

    [Test]
    public void ValidatePiece_OpponentPieceChoice_ReturnsFalse()
    {
        // Arrange
        var whitePawn = new Pawn(true, Color.White, new Cell(2, 'A'), 1);
        var blackPawn = new Pawn(true, Color.Black, new Cell(3, 'A'), 1);
        var cellChoice = new Cell(3, 'A');

        // Add black pawn to pieces list
        _pieces.Add(whitePawn);
        _pieces.Add(blackPawn);

        // Act
        bool result = _gameController.ValidatePiece(cellChoice);

        // Assert
        Assert.That(result, Is.False, "Should return false when cell choice has opponent's piece");
    }

    [Test]
    public void ValidatePiece_CellChoiceOutOfBounds_ReturnsFalse()
    {
        var cellChoice = new Cell(10, 'A');
        bool result = _gameController.ValidatePiece(cellChoice);

        Assert.That(result, Is.False, "Should return false when cell choice out of bounds");
    }

    [Test]
    public void ValidatePiece_CellChoice_ReturnsTrue()
    {
        // Arrange
        var whitePawn1 = new Pawn(true, Color.White, new Cell(2, 'A'), 1);
        var blackPawn = new Pawn(true, Color.Black, new Cell(4, 'A'), 1);
        var cellChoice = new Cell(2, 'A');

        // Add second white pawn to pieces list
        _pieces.Add(blackPawn);
        _pieces.Add(whitePawn1);

        // Act
        bool result = _gameController.ValidatePiece(cellChoice);

        // Assert
        Assert.That(result, Is.True, "Should return true when cell choice has piece with same color");
    }
    
}
