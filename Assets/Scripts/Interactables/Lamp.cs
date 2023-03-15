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
        StartCoroutine(DialogueSystemManager.Instance.DisplayDialogue(DialogueSystemManager.Instance.GetDialogue("First Scene"), 0.01f, 2.0f, 2.0f));
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
