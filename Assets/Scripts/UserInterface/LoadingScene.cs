using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        LoadingScreen.SetActive(true);

        float timeout = 10f;
        float elapsedTime = 0f;

        while (!operation.isDone && elapsedTime < timeout)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progress;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (elapsedTime >= timeout)
        {
            Debug.LogError("Loading operation timed out.");

            LoadingScreen.SetActive(false);
            yield break; // exit the coroutine
        }

        operation.allowSceneActivation = true; // set to true when the scene is fully loaded
    }
}
