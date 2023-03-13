using UnityEngine.Rendering.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMaze : MonoBehaviour
{
    public Camera mainCamera;
    public Vector2 cameraOffset;
    public float bloomIntensity;
    public Material basicMaterial;
    public Material playerMaterial;
    public Material enemyMaterial;
    public PostProcessProfile playerProfile;
    public PostProcessProfile enemyProfile;
    public int width, height = 10;
    private Maze maze;
    public Method method;
    public GameObject cellPrefab;
    public GameObject[,] mazeCells;

    private IEnumerator playerCoroutine;
    private IEnumerator enemyCoroutine;

    private void Awake()
    {
        maze = new(width, height);
        maze.Generate(method);
        Canvas cellCanvas = cellPrefab.GetComponent<Canvas>();
        cellCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        cellCanvas.worldCamera = mainCamera;
    }

    private void Start()
    {
        GameObject cell = cellPrefab.transform.Find("Cell").gameObject;
        RectTransform cellTransform = cell.GetComponent<RectTransform>();
        Vector2 offset = cellTransform.sizeDelta;
        mazeCells = new GameObject[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject currentCell = Instantiate(cellPrefab, Vector3.zero, cellPrefab.transform.rotation);
                currentCell.transform.Find("Cell").gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * offset.x - cameraOffset.x, j * offset.y - cameraOffset.y);
                mazeCells[i, j] = currentCell;
            }
        }
        playerCoroutine = MenuBackgroundAnimation(mazeCells, Vector2.zero, playerMaterial, playerProfile, 0.75f);
        enemyCoroutine = MenuBackgroundAnimation(mazeCells, new Vector2(maze.board.width - 1, maze.board.height - 1), enemyMaterial, enemyProfile, 1.25f);
        StartCoroutine(playerCoroutine);
        StartCoroutine(enemyCoroutine);
    }

    public void ResetPrefab(GameObject prefab)
    {
        foreach (Transform child in prefab.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private IEnumerator MenuBackgroundAnimation(GameObject[,] items, Vector2 startingPoint, Material material, PostProcessProfile profile, float time)
    {
        Cell start = maze.board.grid[(int)startingPoint.x, (int)startingPoint.y];
        Cell current = start;
        GameObject currentObject;
        while (true)
        {
            currentObject = items[current.x, current.y].transform.Find("Cell").gameObject;
            ChangeMaterial(currentObject, material);
            Cell neigbour = GetRandomNeigbour(current);
            yield return new WaitForSeconds(time);
            ChangeMaterial(currentObject, basicMaterial);
            current = neigbour;
        }
    }

    private Cell GetRandomNeigbour(Cell cell)
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

        return neighbours.Count > 0 ? neighbours[UnityEngine.Random.Range(0, neighbours.Count)] : null;
    }

    public void ChangeMaterial(GameObject item, Material material)
    {
        Image itemImage = item.GetComponent<Image>();
        itemImage.material = material;
    }

    public void ChangePostProcessProfile(GameObject item, PostProcessProfile profile)
    {
        item.GetComponent<PostProcessVolume>().profile = profile;
    }
}
