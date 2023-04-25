using System.Collections;
using UnityEngine;

public class Battery : Interactable
{
    public float charge = 50f;

    protected override void Interact()
    {
        FindObjectOfType<Flashlight>().ChargeBattery(charge);
        StartCoroutine(DisplayBatteryLootDialogue());
    }

    IEnumerator DisplayBatteryLootDialogue()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.layer = 0;
        yield return DialogueSystemManager.Instance.DisplayDialogue(DialogueSystemManager.Instance.GetDialogue("LootBattery"), 0.1f, 0.1f, 5f);
        gameObject.SetActive(false);
    }
}
