using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        if (doorOpen)
        {
            promptMessage.Equals("Close door");
        }
        if (!doorOpen)
        {
            promptMessage.Equals("Open door");
        }
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
