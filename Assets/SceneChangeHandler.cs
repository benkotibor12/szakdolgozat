using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeHandler : MonoBehaviour
{
    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;

    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.ResetInitilizedMazeCells();
            gameManager.MazeWidth = gameManager.MazeWidth;
            gameManager.MazeHeight = gameManager.MazeHeight;

            // Set the maze generation method and the next scene to load based on the loaded scene
            switch (scene.name)
            {
                case "Kruskal":
                    gameManager.MazeGenerationMethod = Method.Kruskal;
                    gameManager.NextScene = "RecursiveBacktracking";
                    break;
                case "Prim":
                    gameManager.MazeGenerationMethod = Method.Prim;
                    gameManager.NextScene = "Kruskal";
                    break;
                case "RecursiveBacktracking":
                    gameManager.MazeGenerationMethod = Method.RecursiveBacktracking;
                    gameManager.NextScene = "MainMenu";
                    break;
                case "Wilson":
                    gameManager.MazeGenerationMethod = Method.Wilson;
                    gameManager.NextScene = "Prim";
                    break;
            }

            Debug.Log(gameManager.NextScene);
        }
    }
}
