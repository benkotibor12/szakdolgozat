using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Interactable
{
    [SerializeField]
    private GameObject bulb;
    private bool isOn;
    protected override void Interact()
    {
        isOn = !isOn;
        if (isOn)
        {
            bulb.GetComponent<Light>().intensity = 3;
        }
        else
        {
            bulb.GetComponent<Light>().intensity = 0;
        }
    }
}
