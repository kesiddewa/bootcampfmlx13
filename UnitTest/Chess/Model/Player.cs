namespace Chess;

public class Player : IPlayer
{
    public Color side { get; set; }
    public Status status { get; set; }

    public Player(Color side, Status status)
    {
        this.side = side;
        this.status = status;
    }

    public Color GetColor() => side;
    public Status GetStatus() => status;
    public void SetStatus(Status status) => this.status = status;
}