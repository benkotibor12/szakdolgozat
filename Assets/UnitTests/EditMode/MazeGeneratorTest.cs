using NUnit.Framework;

public class MazeGeneratorTest
{
    [Test]
    public void WilsonAlgorithmTest()
    {
        // Arrange
        Maze maze = new Maze(5, 5);
        int expectedPathways = (maze.width * maze.height) - 1;

        // Act
        maze.Generate(Method.Wilson);

        // Assert
        int actualPathways = CountPathways(maze);
        Assert.AreEqual(expectedPathways, actualPathways);
    }

    [Test]
    public void KruskalAlgorithmTest()
    {
        // Arrange
        Maze maze = new Maze(5, 5);
        int expectedPathways = (maze.width * maze.height) - 1;

        // Act
        maze.Generate(Method.Kruskal);

        // Assert
        int actualPathways = CountPathways(maze);
        Assert.AreEqual(expectedPathways, actualPathways);
    }

    [Test]
    public void PrimAlgorithmTest()
    {
        // Arrange
        Maze maze = new Maze(5, 5);
        int expectedPathways = (maze.width * maze.height) - 1;

        // Act
        maze.Generate(Method.Prim);

        // Assert
        int actualPathways = CountPathways(maze);
        Assert.AreEqual(expectedPathways, actualPathways);
    }

    [Test]
    public void RecursiveBacktrackingAlgorithmTest()
    {
        // Arrange
        Maze maze = new Maze(5, 5);
        int expectedPathways = (maze.width * maze.height) - 1;

        // Act
        maze.Generate(Method.RecursiveBacktracking);

        // Assert
        int actualPathways = CountPathways(maze);
        Assert.AreEqual(expectedPathways, actualPathways);
    }

    private int CountPathways(Maze maze)
    {
        int actualPathways = 0;
        for (int i = 0; i < maze.width; i++)
        {
            for (int j = 0; j < maze.height; j++)
            {
                if (!maze.board.grid[i, j].walls.left)
                {
                    actualPathways++;
                }
                if (!maze.board.grid[i, j].walls.right)
                {
                    actualPathways++;
                }
                if (!maze.board.grid[i, j].walls.top)
                {
                    actualPathways++;
                }
                if (!maze.board.grid[i, j].walls.bottom)
                {
                    actualPathways++;
                }
            }
        }
        actualPathways /= 2; // Divide by 2 to count each pathway only once
        return actualPathways;
    }
}
