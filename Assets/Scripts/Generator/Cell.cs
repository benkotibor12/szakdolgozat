public struct Walls
{
    public bool left { get; set; }
    public bool right { get; set; }
    public bool top { get; set; }
    public bool bottom { get; set; }
}

public class Cell
{
    public int x, y;
    public bool visited;
    public Walls walls;
    public int index;

    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
        visited = false;
        walls.left = true;
        walls.right = true;
        walls.top = true;
        walls.bottom = true;
        index = -1;
    }

    public int GetActiveWalls()
    {
        int count = 0;

        if (walls.left) count++;
        if (walls.right) count++;
        if (walls.top) count++;
        if (walls.bottom) count++;

        return count;
    }

    public void SetIndex(int index)
    {
        if (this.index == -1)
        {
            this.index = index;
        }
    }
}
