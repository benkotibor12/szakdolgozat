using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Method mazeGenerationMethod;
    [SerializeField] private int mazeWidth = 5;
    [SerializeField] private int mazeHeight = 5;
    [SerializeField] private string nextScene;

    public Method MazeGenerationMethod { get => mazeGenerationMethod; set => mazeGenerationMethod = value; }
    public int MazeWidth { get => mazeWidth; set => mazeWidth = value; }
    public int MazeHeight { get => mazeHeight; set => mazeHeight = value; }
    public string NextScene { get => nextScene; set => nextScene = value; }

    public static GameManager Instance { get; private set; }

    private List<GameObject> initializedCells = new();
    private LoadingScene loadingScene;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        loadingScene = GetComponent<LoadingScene>();
    }

    public void LoadScene(string sceneName)
    {
        loadingScene.LoadScene(sceneName);
    }

    public void AddInitializedCell(GameObject cell)
    {
        initializedCells.Add(cell);
    }

    public void AddInitializedCells(List<GameObject> cells)
    {
        initializedCells.AddRange(cells);
    }

    public List<GameObject> GetInitializedCells() => initializedCells;

    public void ResetInitilizedMazeCells()
    {
        initializedCells.Clear();
    }
}
