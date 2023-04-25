[System.Serializable]
public class GameData
{
    public string currentScene;
    public float audioVolume;
    public float horizontalSensitivity;
    public float verticalSensitivity;
    public float brightness;
    public bool isFullScreen;
    public int quaility;
    public int resolution;


    public GameData()
    {
        currentScene = "";
        audioVolume = 0.0f;
        horizontalSensitivity = 30.0f;
        verticalSensitivity = 30.0f;
        brightness = 1.0f;
        quaility = 0;
        resolution = 1;
    }
}