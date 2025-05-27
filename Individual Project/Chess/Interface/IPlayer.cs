namespace Chess;

public interface IPlayer
{
    Color side { get; }
    Status status { get; set; }
}