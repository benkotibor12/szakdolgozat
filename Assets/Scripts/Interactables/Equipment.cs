using UnityEngine;

public class Equipment : Interactable, IInventoryItem
{
    [SerializeField] private string itemName = "Gun";
    [SerializeField] private string keybind = "1";
    [SerializeField] private Sprite image;

    public string Name => itemName;

    public Sprite Image => image;

    public string Keybind => keybind;

    public void OnPickup()
    {
        gameObject.SetActive(false);
        gameObject.layer = 0;
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
