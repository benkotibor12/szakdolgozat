using UnityEngine;

public class Equipment : Interactable, IInventoryItem
{
    [SerializeField] private string itemName;
    [SerializeField] private string keybind;
    [SerializeField] private Sprite image;
    [SerializeField] private bool inUse;
    public Vector3 PickUpPosition;
    public Vector3 PickUpRotation;
    public Vector3 PickUpScale;

    public string Name => itemName;

    public Sprite Image => image;

    public string Keybind => keybind;

    public bool InUse => inUse;

    Vector3 IInventoryItem.PickUpPosition => PickUpPosition;

    Vector3 IInventoryItem.PickUpRotation => PickUpRotation;

    Vector3 IInventoryItem.PickUpScale => PickUpScale;

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

    
}
