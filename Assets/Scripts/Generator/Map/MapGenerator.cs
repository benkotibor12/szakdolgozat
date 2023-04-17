using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public bool debugMode = false;
    [HideInInspector] public Maze maze;
    [HideInInspector] public GameObject left, right, top, bottom, floor;
    private PlatformUI platformUI;

    private void Start()
    {
        platformUI = platformPrefab.GetComponent<PlatformUI>();
        GenerateMaze();
        SetupPrefab();
        InitializeMaze();
    }

    public void GenerateMaze()
    {
        maze = new Maze(GameManager.Instance.MazeWidth, GameManager.Instance.MazeHeight);
        maze.Generate(GameManager.Instance.MazeGenerationMethod);
    }

    public void SetupPrefab()
    {
        left = platformPrefab.transform.Find("Left").gameObject;
        right = platformPrefab.transform.Find("Right").gameObject;
        top = platformPrefab.transform.Find("Top").gameObject;
        bottom = platformPrefab.transform.Find("Bottom").gameObject;
        floor = platformPrefab.transform.Find("Floor").gameObject;
    }

    public void InitializeMaze()
    {
        float offsetX = floor.transform.localScale.x * platformPrefab.transform.localScale.x;
        float offsetZ = floor.transform.localScale.z * platformPrefab.transform.localScale.z;

        for (int i = 0; i < GameManager.Instance.MazeWidth; i++)
        {
            for (int j = 0; j < GameManager.Instance.MazeHeight; j++)
            {
                UpdateWallState(i, j);
                UpdatePlatformUI(i, j);

                GameObject prefabInstance = Instantiate(platformPrefab, new Vector3(i * offsetZ, 0, j * offsetX), platformPrefab.transform.rotation);

                if (maze.startX == i && maze.startY == j)
                {
                    prefabInstance.transform.Find("Floor").gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    prefabInstance.tag = "StartingCell";
                }

                if (maze.board.grid[i, j].isExit)
                {
                    prefabInstance.transform.Find("Floor").gameObject.GetComponent<Renderer>().material.color = Color.red;
                    prefabInstance.tag = "ExitCell";
                }

                ResetPrefab(platformPrefab);
                GameManager.Instance.AddInitializedCell(prefabInstance);
            }
        }
    }

    private void UpdateWallState(int i, int j)
    {
        if (!maze.board.grid[i, j].walls.left)
        {
            left.SetActive(false);
        }
        if (!maze.board.grid[i, j].walls.right)
        {
            right.SetActive(false);
        }
        if (!maze.board.grid[i, j].walls.top)
        {
            top.SetActive(false);
        }
        if (!maze.board.grid[i, j].walls.bottom)
        {
            bottom.SetActive(false);
        }
    }

    private void UpdatePlatformUI(int i, int j)
    {
        if (debugMode)
        {
            platformUI.UpdateText(maze.board.grid[i, j].index.ToString());
        }
        else
        {
            platformUI.UpdateText("");
        }
    }

    public void ResetPrefab(GameObject prefab)
    {
        foreach (Transform child in prefab.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
