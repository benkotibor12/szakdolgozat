using System;
using System.Collections.Generic;
using UnityEngine;

public enum Method
{
    RecursiveBacktracking,
    Kruskal,
    Prim
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
                break;
            case Method.Prim:
                break;
        }
    }

    public void RecursiveBacktracker()
    {
        Cell current = board.grid[startX, startY];
        Cell next;
        Stack<Cell> route = new Stack<Cell>();
        route.Push(current);
        while (route.Count > 0)
        {
            current.visited = true;
            next = GetAvailableNeighbour(current);
            if (next != null)
            {
                next.visited = true;
                RemoveWallBetween(current, next);
                route.Push(next);
                current = next;
            }
            else if (next == null)
            {
                current = route.Pop();
            }
        }
        Debug.Log("Done");
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

    public Cell GetAvailableNeighbour(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();
        if (cell.x - 1 >= 0)
        {
            if (!board.grid[cell.x - 1, cell.y].visited)
            {
                neighbours.Add(board.grid[cell.x - 1, cell.y]);
            }
        }
        if (cell.x + 1 < board.width)
        {
            if (!board.grid[cell.x + 1, cell.y].visited)
            {
                neighbours.Add(board.grid[cell.x + 1, cell.y]);
            }
        }
        if (cell.y - 1 >= 0)
        {
            if (!board.grid[cell.x, cell.y - 1].visited)
            {
                neighbours.Add(board.grid[cell.x, cell.y - 1]);
            }
        }
        if (cell.y + 1 < height)
        {
            if (!board.grid[cell.x, cell.y + 1].visited)
            {
                neighbours.Add(board.grid[cell.x, cell.y + 1]);
            }
        }
        return neighbours.Count > 0 ? neighbours[UnityEngine.Random.Range(0, neighbours.Count)] : null;
    }
}
