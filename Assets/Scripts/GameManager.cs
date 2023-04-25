using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Method mazeGenerationMethod;
    [SerializeField] private int mazeWidth = 5;
    [SerializeField] private int mazeHeight = 5;
    [SerializeField] private string nextScene;
    [SerializeField] private string currentScene;
    [SerializeField] private Transform playerSpawnPoint;

    public Method MazeGenerationMethod { get => mazeGenerationMethod; set => mazeGenerationMethod = value; }
    public int MazeWidth { get => mazeWidth; set => mazeWidth = value; }
    public int MazeHeight { get => mazeHeight; set => mazeHeight = value; }
    public string NextScene { get => nextScene; set => nextScene = value; }
    public string CurrentScene { get => currentScene; set => currentScene = value; }
    public Transform PlayerSpawnPoint { get => playerSpawnPoint; set => playerSpawnPoint = value; }

    public static GameManager Instance { get; private set; }

    private List<GameObject> initializedCells = new();
    private LoadingScene loadingScene;
    private float brightness;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        loadingScene = GetComponent<LoadingScene>();
        CurrentScene = SceneManager.GetActiveScene().name;
        InitScene(CurrentScene);
        RenderSettings.ambientLight = RenderSettings.ambientLight * brightness;
        StartCoroutine(DisplayCurrentQuest());
    }

    IEnumerator DisplayCurrentQuest()
    {
        yield return DialogueSystemManager.Instance.DisplayDialogue(DialogueSystemManager.Instance.GetDialogue(currentScene), 0.1f, 0.1f, 5f, true);
    }

    public void LoadScene(string sceneName)
    {
        loadingScene.LoadScene(sceneName);
    }

    public void InitScene(string sceneName)
    {
        if (!CurrentScene.Equals(sceneName) && !CurrentScene.Equals(""))
        {
            loadingScene.LoadScene(CurrentScene);
        }
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

    public void LoadData(GameData gameData)
    {
        CurrentScene = gameData.currentScene;
        brightness = gameData.brightness;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.currentScene = CurrentScene;
    }

    public bool IsMapInitialized()
    {
        return initializedCells.Count == mazeWidth * mazeHeight;
    }
}
