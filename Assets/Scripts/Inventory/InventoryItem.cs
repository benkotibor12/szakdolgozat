using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    string Keybind { get; }
    Sprite Image { get; }
    Vector3 PickUpPosition { get; }
    Vector3 PickUpRotation { get; }
    Vector3 PickUpScale { get; }
    bool InUse { get; }
    void OnPickup();
    void OnSelect();
    void OnUse();
}

public class InventoryEventArgs : System.EventArgs
{
    public IInventoryItem Item;

    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
}
