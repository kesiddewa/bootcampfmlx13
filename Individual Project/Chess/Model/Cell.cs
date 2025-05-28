public struct Cell
{
    public int row { get; set; }
    public char column { get; set; }

    public Cell(int row, char column)
    {
        this.row = row;
        this.column = column;
    }
}