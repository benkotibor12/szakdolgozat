using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IDataPersistence
{
    public Button newGameButton;
    public Button loadGameButton;
    private string loadedScene = "";
    private LoadingScene loadingScene;

    private void Start()
    {
        StartCoroutine(WaitForLoadingData());
        loadingScene = GetComponent<LoadingScene>();
    }

    IEnumerator WaitForLoadingData()
    {
        yield return new WaitForEndOfFrame();

        if (!loadedScene.Equals(""))
        {
            newGameButton.enabled = false;
            newGameButton.image.color = new Color(30f / 255f, 0f, 0f);
        }
        else
        {
            loadGameButton.enabled = false;
            loadGameButton.image.color = new Color(30f / 255f, 0f, 0f);
        }
    }

    public void PlayGame()
    {
        loadingScene.LoadScene("Wilson");
    }

    public void LoadGame()
    {
        loadingScene.LoadScene(loadedScene);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }

    public void LoadData(GameData gameData)
    {
        loadedScene = gameData.currentScene;
    }

    public void SaveData(ref GameData gameData)
    {
        // TODO: save options
    }
}
