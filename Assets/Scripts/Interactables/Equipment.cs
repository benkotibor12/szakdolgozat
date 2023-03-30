using UnityEngine;

public class Equipment : Interactable, IInventoryItem
{
    [SerializeField] private string itemName;
    [SerializeField] private string keybind;
    [SerializeField] private Sprite image;
    [SerializeField] private bool inUse;

    public string Name => itemName;

    public Sprite Image => image;

    public string Keybind => keybind;

    public bool InUse => inUse;

    public void OnPickup()
    {
        gameObject.SetActive(false);
        gameObject.layer = 0;
        inUse = false;
    }

    public void OnSelect()
    {
        Debug.Log(name);
        //sound, animation
    }

    public void OnUse()
    {
        inUse = !inUse;
        transform.Find("Light").gameObject.SetActive(inUse);
    }

    protected override void Interact()
    {
        promptMessage.Equals("Collect");
    }

    public Vector3 PickUpPosition;

    public Vector3 PickUpRotation;

    public Vector3 PickUpScale;
}
