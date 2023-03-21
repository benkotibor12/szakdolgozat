using UnityEngine;

public class Flashlight : Interactable, IInventoryItem
{
    [SerializeField] private string name = "Flashlight";
    [SerializeField] private string keybind = "3";
    [SerializeField] private Sprite image;

    public string Name => name;

    public Sprite Image => image;

    public string Keybind => keybind;

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public void OnSelect()
    {
        Debug.Log(name);
        //sound, animation
    }

    public void OnUse()
    {
        Debug.Log("vilagitok");
    }

    protected override void Interact()
    {
        promptMessage.Equals("Collect");
    }
}
