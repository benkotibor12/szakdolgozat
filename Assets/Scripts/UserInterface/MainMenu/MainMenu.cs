using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IDataPersistence
{
    public Button newGameButton;
    public Button loadGameButton;
    private string loadedScene = "";

    private void Start()
    {
        StartCoroutine(WaitForLoadingData());
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(loadedScene);
    }

    public void QuitGame()
    {
        Application.Quit();
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
