using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private LoadingScene loadingScene;
    private void Start()
    {
        loadingScene = GetComponent<LoadingScene>();
    }

    public void LoadScene(string sceneName)
    {
        loadingScene.LoadScene(sceneName);
    }
}
