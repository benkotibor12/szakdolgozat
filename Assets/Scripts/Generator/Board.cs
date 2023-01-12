public class Board
{
    public Cell[,] grid;
    public int width, height;

    public Board(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new Cell[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Cell cell = new Cell(i, j);
                grid[i, j] = cell;
            }
        }
    }


}


