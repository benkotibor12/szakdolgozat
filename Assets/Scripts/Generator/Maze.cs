using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Method
{
    RecursiveBacktracking,
    Kruskal,
    Prim,
    Wilson
}

public class Maze
{
    public int width, height;
    public int startX, startY = 0;
    public Board board;

    public Maze(int width, int height)
    {
        this.width = width;
        this.height = height;
        board = new Board(width, height);
    }

    public void Generate(Method method)
    {
        switch (method)
        {
            case Method.RecursiveBacktracking:
                RecursiveBacktracker();
                break;
            case Method.Kruskal:
                Kruskal();
                List<Cell> path = FindLongestPath();
                break;
            case Method.Prim:
                Prim();
                break;
            case Method.Wilson:
                Wilson();
                break;
        }
    }

    private void Wilson()
    {
        List<Cell> unvisitedCells = new();
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                unvisitedCells.Add(board.grid[i, j]);
            }
        }

        Cell start = unvisitedCells[UnityEngine.Random.Range(0, unvisitedCells.Count)];
        unvisitedCells.Remove(start);

        while (unvisitedCells.Count > 0)
        {
            Cell current = unvisitedCells[UnityEngine.Random.Range(0, unvisitedCells.Count)];
            Stack<Cell> path = new();
            path.Push(current);
            while (unvisitedCells.Contains(current))
            {
                Cell next = GetNeigbour(current, false);
                if (path.Contains(next))
                {
                    Cell pop = new(-1, -1);
                    while (pop != next)
                    {
                        pop = path.Pop();
                    }
                }
                path.Push(next);
                current = next;
            }

            Cell[] pathArray = path.ToArray();
            for (int i = 0; i < path.Count - 1; i++)
            {
                RemoveWallBetween(pathArray[i], pathArray[i + 1]);
            }
            for (int i = 0; i < path.Count; i++)
            {
                unvisitedCells.Remove(pathArray[i]);
            }
        }
    }

    private void Kruskal()
    {
        int wallsDown = 0;

        Cell current;
        Cell neighbour;
        DisjointSet disjointSet = new DisjointSet(board.height * board.width);

        while (wallsDown < board.height * board.width - 1)
        {
            current = board.grid[UnityEngine.Random.Range(0, board.width), UnityEngine.Random.Range(0, board.height)];
            neighbour = GetNeigbourWithWalls(current);
            if (neighbour != null)
            {
                if (disjointSet.Find(current.index) != disjointSet.Find(neighbour.index))
                {
                    disjointSet.Union(current.index, neighbour.index);
                    neighbour.label = current.label;
                    RemoveWallBetween(current, neighbour);
                    wallsDown += 1;
                }
            }
        }
    }

    public void Prim()
    {
        HashSet<Cell> frontiers = new HashSet<Cell>();
        List<Cell> visitedCells = new List<Cell>();
        Cell first = board.grid[startX, startY];
        Cell frontier;
        frontiers.Add(first);
        Cell selected = first;
        int counter = 0;
        while (frontiers.Count > 0)
        {
            counter++;
            selected.SetIndex(counter);
            frontiers.Remove(selected);
            selected.visited = true;
            visitedCells.Add(selected);
            if (visitedCells.Count == board.width * board.height)
            {
                break;
            }
            if (GetFrontiers(selected) != null)
            {
                frontiers.UnionWith(GetFrontiers(selected));
            }
            Cell[] frotiersArray = frontiers.ToArray();
            frontier = frotiersArray[UnityEngine.Random.Range(0, frontiers.Count)];
            Cell neighbour = GetNeigbour(frontier, true);
            RemoveWallBetween(frontier, neighbour);
            selected = frontier;
        }
    }

    public void RecursiveBacktracker()
    {
        Cell current = board.grid[startX, startY];
        Cell next;
        Stack<Cell> route = new Stack<Cell>();
        route.Push(current);
        int counter = 0;
        while (route.Count > 0)
        {
            current.visited = true;
            next = GetNeigbour(current, false);
            if (next != null)
            {
                next.visited = true;
                counter++;
                current.SetIndex(counter);
                RemoveWallBetween(current, next);
                route.Push(next);
                current = next;
            }
            else if (next == null)
            {
                current = route.Pop();
            }
        }
    }

    private List<Cell> GetFrontiers(Cell cell)
    {
        List<Cell> frontiers = new();
        if (cell.x - 1 >= 0)
        {
            if (!board.grid[cell.x - 1, cell.y].visited)
            {
                frontiers.Add(board.grid[cell.x - 1, cell.y]);
            }
        }
        if (cell.x + 1 < board.width)
        {
            if (!board.grid[cell.x + 1, cell.y].visited)
            {
                frontiers.Add(board.grid[cell.x + 1, cell.y]);
            }
        }
        if (cell.y - 1 >= 0)
        {
            if (!board.grid[cell.x, cell.y - 1].visited)
            {
                frontiers.Add(board.grid[cell.x, cell.y - 1]);
            }
        }
        if (cell.y + 1 < board.height)
        {
            if (!board.grid[cell.x, cell.y + 1].visited)
            {
                frontiers.Add(board.grid[cell.x, cell.y + 1]);
            }
        }

        return frontiers.Count > 0 ? frontiers : null;
    }

    private List<Cell> GetNeighbours(Cell cell)
    {
        List<Cell> frontiers = new();
        if (cell.x - 1 >= 0)
        {
            frontiers.Add(board.grid[cell.x - 1, cell.y]);
        }
        if (cell.x + 1 < board.width)
        {
            frontiers.Add(board.grid[cell.x + 1, cell.y]);
        }
        if (cell.y - 1 >= 0)
        {
            frontiers.Add(board.grid[cell.x, cell.y - 1]);
        }
        if (cell.y + 1 < board.height)
        {
            frontiers.Add(board.grid[cell.x, cell.y + 1]);
        }

        return frontiers.Count > 0 ? frontiers : null;
    }

    public bool HasWallBetween(Cell a, Cell b)
    {
        if (Math.Abs(a.x - b.x) == 1 ^ Math.Abs(a.y - b.y) == 1)
        {
            if (a.x > b.x)
            {
                if (a.walls.left && b.walls.right)
                {
                    return true;
                }
            }
            if (a.x < b.x)
            {
                if (a.walls.right && b.walls.left)
                {
                    return true;
                }
            }
            if (a.y > b.y)
            {
                if (a.walls.bottom && b.walls.top)
                {
                    return true;
                }
            }
            if (a.y < b.y)
            {
                if (a.walls.top && b.walls.bottom)
                {
                    return true;
                }
            }
        }
        else if (a == b)
        {
            Debug.LogWarning("Same cell!");
        }

        return false;
    }

    public void RemoveWallBetween(Cell a, Cell b)
    {
        if (Math.Abs(a.x - b.x) == 1 ^ Math.Abs(a.y - b.y) == 1)
        {
            if (a.x > b.x)
            {
                a.walls.left = false;
                b.walls.right = false;
            }
            if (a.x < b.x)
            {
                a.walls.right = false;
                b.walls.left = false;
            }
            if (a.y > b.y)
            {
                a.walls.bottom = false;
                b.walls.top = false;
            }
            if (a.y < b.y)
            {
                a.walls.top = false;
                b.walls.bottom = false;
            }
        }
        else if (a == b)
        {
            Debug.LogWarning("Same cell!");
        }
        else
        {
            Debug.LogError("Can not remove wall!");
        }
    }

    public void RemoveWallsBetween(Cell a, Cell b)
    {
        Vector2 direction = new Vector2(a.x, a.y) - new Vector2(b.x, b.y);

        if (direction == Vector2.left)
        {
            a.walls.right = false;
            b.walls.left = false;
        }
        if (direction == Vector2.right)
        {
            a.walls.left = false;
            b.walls.right = false;
        }
        if (direction == Vector2.up)
        {
            a.walls.bottom = false;
            b.walls.top = false;
        }
        if (direction == Vector2.down)
        {
            a.walls.top = false;
            b.walls.bottom = false;
        }
    }

    public Cell GetNeigbourWithWalls(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();
        List<Cell> neighboursWithWalls = new List<Cell>();
        if (cell.x - 1 >= 0) neighbours.Add(board.grid[cell.x - 1, cell.y]);
        if (cell.x + 1 < board.width) neighbours.Add(board.grid[cell.x + 1, cell.y]);
        if (cell.y - 1 >= 0) neighbours.Add(board.grid[cell.x, cell.y - 1]);
        if (cell.y + 1 < height) neighbours.Add(board.grid[cell.x, cell.y + 1]);

        foreach (Cell neighbour in neighbours)
        {
            if (HasWallBetween(cell, neighbour))
            {
                neighboursWithWalls.Add(neighbour);
            }
        }

        return neighboursWithWalls.Count > 0 ? neighboursWithWalls[UnityEngine.Random.Range(0, neighboursWithWalls.Count)] : null;
    }

    public Cell GetNeigbour(Cell cell, bool isVisited = false)
    {
        List<Cell> neighbours = new List<Cell>();
        if (cell.x - 1 >= 0)
        {
            if (isVisited)
            {
                if (board.grid[cell.x - 1, cell.y].visited)
                {
                    neighbours.Add(board.grid[cell.x - 1, cell.y]);
                }
            }
            else
            {
                if (!board.grid[cell.x - 1, cell.y].visited)
                {
                    neighbours.Add(board.grid[cell.x - 1, cell.y]);
                }
            }
        }
        if (cell.x + 1 < board.width)
        {
            if (isVisited)
            {
                if (board.grid[cell.x + 1, cell.y].visited)
                {
                    neighbours.Add(board.grid[cell.x + 1, cell.y]);
                }
            }
            else
            {
                if (!board.grid[cell.x + 1, cell.y].visited)
                {
                    neighbours.Add(board.grid[cell.x + 1, cell.y]);
                }
            }
        }
        if (cell.y - 1 >= 0)
        {
            if (isVisited)
            {
                if (board.grid[cell.x, cell.y - 1].visited)
                {
                    neighbours.Add(board.grid[cell.x, cell.y - 1]);
                }
            }
            else
            {
                if (!board.grid[cell.x, cell.y - 1].visited)
                {
                    neighbours.Add(board.grid[cell.x, cell.y - 1]);
                }
            }
        }
        if (cell.y + 1 < height)
        {
            if (isVisited)
            {
                if (board.grid[cell.x, cell.y + 1].visited)
                {
                    neighbours.Add(board.grid[cell.x, cell.y + 1]);
                }
            }
            else
            {
                if (!board.grid[cell.x, cell.y + 1].visited)
                {
                    neighbours.Add(board.grid[cell.x, cell.y + 1]);
                }
            }
        }

        return neighbours.Count > 0 ? neighbours[UnityEngine.Random.Range(0, neighbours.Count)] : null;
    }

    public List<Cell> FindLongestPath()
    {
        ResetVisitedStatus();
        Cell start = board.grid[startX, startY];
        List<Cell> longestPath = new();
        DFS(start, ref longestPath);
        DebugPath(longestPath);
        return longestPath;
    }

    private void DFS(Cell start, ref List<Cell> longestPath)
    {
        Stack<(Cell, List<Cell>)> stack = new Stack<(Cell, List<Cell>)>();
        stack.Push((start, new List<Cell>()));

        while (stack.Count > 0)
        {
            (Cell current, List<Cell> path) = stack.Pop();

            if (!current.visited)
            {
                current.visited = true;
                path.Add(current);

                if (path.Count > longestPath.Count)
                {
                    longestPath.Clear();
                    longestPath.AddRange(path);
                }

                foreach (Cell neighbor in GetNeighbours(current))
                {
                    if (!neighbor.visited && !HasWallBetween(current, neighbor))
                    {
                        stack.Push((neighbor, new List<Cell>(path)));
                    }
                }
            }
        }
    }

    public void DebugPath(List<Cell> path)
    {
        foreach (Cell cell in path)
        {
            cell.isExit = true;
        }
    }
    private void ResetVisitedStatus()
    {
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                board.grid[i, j].visited = false;
            }
        }
    }
}