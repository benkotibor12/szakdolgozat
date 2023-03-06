using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width, height = 10;
    private Maze maze;
    public Method method;
    public GameObject platformPrefab;
    private PlatformUI platformUI;

    GameObject left;
    GameObject right;
    GameObject top;
    GameObject bottom;
    GameObject floor;

    private void Awake()
    {
        maze = new(width, height);
        maze.Generate(method);
        platformUI = platformPrefab.GetComponent<PlatformUI>();
    }
    private void Start()
    {
        left = platformPrefab.transform.Find("Left").gameObject;
        right = platformPrefab.transform.Find("Right").gameObject;
        top = platformPrefab.transform.Find("Top").gameObject;
        bottom = platformPrefab.transform.Find("Bottom").gameObject;
        floor = platformPrefab.transform.Find("Floor").gameObject;
        float offset = floor.transform.localScale.x * platformPrefab.transform.localScale.x;
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
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
                platformUI.UpdateText(maze.board.grid[i, j].label);
                Instantiate(platformPrefab, new Vector3(i * offset, 0, j * offset), platformPrefab.transform.rotation);
                ResetPrefab(platformPrefab);
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
}
