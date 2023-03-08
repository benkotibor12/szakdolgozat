using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMaze : MonoBehaviour
{
    public int width, height = 10;
    private Maze maze;
    public Method method;
    public GameObject cellPrefab;

    private void Awake()
    {
        maze = new(width, height);
        maze.Generate(method);
    }
    private void Start()
    {
        GameObject cell = cellPrefab.transform.Find("Cell").gameObject;
        RectTransform cellTransform = cell.GetComponent<RectTransform>();
        Vector2 offset = cellTransform.sizeDelta;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject currentCell = Instantiate(cellPrefab, Vector3.zero, cellPrefab.transform.rotation);
                currentCell.transform.Find("Cell").gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * offset.x, j * offset.y);
                //ResetPrefab(cellPrefab);
            }
        }
    }

    public void ResetPrefab(GameObject prefab)
    {
        foreach (Transform child in prefab.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public Cell GetRandomWalkableNeighbour(Cell cell)
    {
        List<Cell> neighbours = new();
        if (cell.x - 1 >= 0)
        {
            neighbours.Add(maze.board.grid[cell.x - 1, cell.y]);
        }
        if (cell.x + 1 < maze.board.width)
        {
            neighbours.Add(maze.board.grid[cell.x + 1, cell.y]);
        }
        if (cell.y - 1 >= 0)
        {
            neighbours.Add(maze.board.grid[cell.x, cell.y - 1]);
        }
        if (cell.y + 1 < maze.board.height)
        {
            neighbours.Add(maze.board.grid[cell.x, cell.y + 1]);
        }


        foreach (Cell neighbour in neighbours)
        {
            if (HasWallBetween(cell, neighbour))
            {
                neighbours.Remove(neighbour);
            }
        }

        return neighbours.Count > 0 ? neighbours[UnityEngine.Random.Range(0, neighbours.Count)] : null;
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
}
