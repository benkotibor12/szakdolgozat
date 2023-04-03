using UnityEngine;

public class ExitLock : MonoBehaviour
{
    [SerializeField] private Light leftLight;
    [SerializeField] private Light rightLight;
    [SerializeField] private Color closedColor;
    [SerializeField] private Color openedColor;

    private bool LeverLeftActivated;
    private bool LeverRightActivated;
    public bool gateOpened;

    private void Start()
    {
        leftLight = transform.Find("LeftLight").GetComponent<Light>();
        rightLight = transform.Find("RightLight").GetComponent<Light>();
        LeverLeftActivated = false;
        LeverRightActivated = false;
        gateOpened = false;
    }

    public void RefreshGateStatus()
    {
        if (LeverLeftActivated && LeverRightActivated)
        {
            leftLight.color = openedColor;
            rightLight.color = openedColor;
            gateOpened = true;
        }
        else if (LeverLeftActivated)
        {
            leftLight.color = openedColor;
        }
        else if (LeverRightActivated)
        {
            rightLight.color = openedColor;
        }
        else
        {
            leftLight.color = closedColor;
            rightLight.color = closedColor;
        }
    }

    public void ActivateButton(int button)
    {
        switch (button)
        {
            case 0: LeverLeftActivated = true; break;
            case 1: LeverRightActivated = true; break;
            default:
                break;
        }
    }
}
