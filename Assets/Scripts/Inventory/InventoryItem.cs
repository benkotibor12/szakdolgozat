using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    string Keybind { get; }
    Sprite Image { get; }
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
