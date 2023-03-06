public class Board
{
    public Cell[,] grid;
    public int width, height;
    readonly char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public Board(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new Cell[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                // Calculate label for this cell
                string label = "";
                int index = i * height + j;
                while (true)
                {
                    label = alphabet[index % 26] + label;
                    index = index / 26 - 1;
                    if (index < 0) break;
                }
                label += (j + 1).ToString();

                Cell cell = new Cell(i, j);
                cell.label = label;
                grid[i, j] = cell;
            }
        }
    }
}
