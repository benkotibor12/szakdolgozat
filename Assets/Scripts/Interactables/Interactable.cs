using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;

    public void BaseInteraction()
    {
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
