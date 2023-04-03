using UnityEngine;

public class OpenDoor : Interactable
{
    Animator doorAnimator;
    public string sceneName;
    public float sceneDelay = 1f;

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        sceneName = GameManager.Instance.NextScene;
    }

    protected override void Interact()
    {
        gameObject.layer = 0;
        doorAnimator.SetBool("isOpen", true);
        AudioManager.Instance.Play("OldDoor");
        Invoke(nameof(LoadNextScene), sceneDelay);
    }

    void LoadNextScene()
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}
