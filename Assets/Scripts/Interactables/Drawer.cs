using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : Interactable
{
    [SerializeField]
    private GameObject drawer;
    private bool drawerOpen;

    protected override void Interact()
    {
        drawerOpen = !drawerOpen;
        drawer.GetComponent<Animator>().SetBool("IsOpen", drawerOpen);
    }
}
