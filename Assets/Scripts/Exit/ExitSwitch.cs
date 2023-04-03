using UnityEngine;

public class ExitSwitch : Interactable
{
    public int buttonId;
    private ExitLock exitLock;
    private Animator switchAnimator;
    private bool isTurnedOn;

    private void Start()
    {
        isTurnedOn = false;
        exitLock = FindObjectOfType<ExitLock>();
        switchAnimator = GetComponent<Animator>();
    }

    protected override void Interact()
    {
        switchAnimator.SetBool("TurnSwitch", isTurnedOn);
        AudioManager.Instance.Play("TurnSwitch");
        exitLock.ActivateButton(buttonId);
        exitLock.RefreshGateStatus();
        gameObject.layer = 0;
    }
}
