public class ExitLever : Interactable
{
    public int buttonId;
    private ExitLock exitLock;
    private void Start()
    {
        exitLock = FindObjectOfType<ExitLock>();
    }

    protected override void Interact()
    {
        AudioManager.Instance.Play("LeverPull");
        exitLock.ActivateButton(buttonId);
        exitLock.RefreshGateStatus();
        gameObject.layer = 0;
    }
}
