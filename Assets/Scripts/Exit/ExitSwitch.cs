using UnityEngine;

public class ExitSwitch : Interactable
{
    public int buttonId;
    private ExitLock exitLock;
    private Animator switchAnimator;

    private void Start()
    {
        exitLock = FindObjectOfType<ExitLock>();
        switchAnimator = GetComponent<Animator>();
    }

    protected override void Interact()
    {
        switchAnimator.SetBool("TurnSwitch", true);
        AudioManager.Instance.Play("TurnSwitch");
        exitLock.ActivateButton(buttonId);
        exitLock.RefreshGateStatus();
        gameObject.layer = 0;
    }
}
