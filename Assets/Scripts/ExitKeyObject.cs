public class ExitKeyObject : Interactable
{
    public int buttonId;

    protected override void Interact()
    {
        //sound
        GetComponentInParent<ExitLock>().ActivateButton(buttonId);
        GetComponentInParent<ExitLock>().RefreshGateStatus();
        gameObject.layer = 0;
    }
}
