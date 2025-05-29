namespace Chess;

public interface IPlayer
{
    Color side { get; }
    Status status { get; set; }
    void SetStatus(Status status);
    Color GetColor();
    Status GetStatus();
}