public class ExitObject : Interactable
{
    public string sceneName;

    private void Start()
    {
        sceneName = GameManager.Instance.NextScene;
    }

    protected override void Interact()
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}
