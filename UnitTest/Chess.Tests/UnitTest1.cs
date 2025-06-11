using Chess;

namespace Chess.Tests;

[TestFixture]
public class Tests
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
    public void IsValidDestination_EmptyCell_ReturnsTrue()
    {
        // Arrange
        var whitePawn = new Pawn(true, Color.White, new Cell(2, 'A'), 1);
        var destinationCell = new Cell(3, 'A');

        // Act
        bool result = _gameController.IsValidDestination(whitePawn, destinationCell);

        // Assert
        Assert.That(result, Is.True, "Should return true when destination cell is empty");
    }

    [Test]
    public void IsValidDestination_OpponentPieceAtDestination_ReturnsTrue()
    {
        // Arrange
        var whitePawn = new Pawn(true, Color.White, new Cell(2, 'A'), 1);
        var blackPawn = new Pawn(true, Color.Black, new Cell(3, 'A'), 1);
        var destinationCell = new Cell(3, 'A');

        // Add black pawn to pieces list
        _pieces.Add(blackPawn);

        // Act
        bool result = _gameController.IsValidDestination(whitePawn, destinationCell);

        // Assert
        Assert.That(result, Is.True, "Should return true when destination cell has opponent's piece");
    }

    [Test]
    public void IsValidDestination_SameColorPieceAtDestination_ReturnsFalse()
    {
        // Arrange
        var whitePawn1 = new Pawn(true, Color.White, new Cell(2, 'A'), 1);
        var whitePawn2 = new Pawn(true, Color.White, new Cell(3, 'A'), 2);
        var destinationCell = new Cell(3, 'A');

        // Add second white pawn to pieces list
        _pieces.Add(whitePawn2);

        // Act
        bool result = _gameController.IsValidDestination(whitePawn1, destinationCell);

        // Assert
        Assert.That(result, Is.False, "Should return false when destination cell has same color piece");
    }

    [Test]
    public void IsValidDestination_DeadPieceAtDestination_ReturnsTrue()
    {
        // Arrange
        var whitePawn = new Pawn(true, Color.White, new Cell(2, 'A'), 1);
        var deadBlackPawn = new Pawn(false, Color.Black, new Cell(3, 'A'), 1); // Dead piece
        var destinationCell = new Cell(3, 'A');

        // Add dead black pawn to pieces list
        _pieces.Add(deadBlackPawn);

        // Act
        bool result = _gameController.IsValidDestination(whitePawn, destinationCell);

        // Assert
        Assert.That(result, Is.True, "Should return true when destination cell has dead piece (treated as empty)");
    }
}

