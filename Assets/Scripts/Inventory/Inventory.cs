using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 8;
    private List<IInventoryItem> items = new();
    private IInventoryItem selectedItem;
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;
    public event EventHandler<InventoryEventArgs> ItemSelected;

    public void AddItem(IInventoryItem item)
    {
        if (items.Count < SLOTS)
        {
            items.Add(item);
            item.OnPickup();

            ItemAdded?.Invoke(this, new InventoryEventArgs(item));
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            ItemRemoved?.Invoke(this, new InventoryEventArgs(item));
        }
    }

    public void RemoveItemWithName(string name)
    {
        foreach(IInventoryItem item in items)
        {
            if (item.Name.Equals(name))
            {
                items.Remove(item);
                ItemRemoved?.Invoke(this, new InventoryEventArgs(item));
            }
        }
    }

    internal void SelectItem(IInventoryItem item)
    {
        if (items.Contains(item))
        {
            //Deselect the equipped item before selecting a new one
            if(selectedItem != null)
            {
                GameObject itemAsGameObject = (selectedItem as MonoBehaviour).gameObject;
                itemAsGameObject.SetActive(false);
            }
            selectedItem = item;
            item.OnSelect();
            ItemSelected?.Invoke(this, new InventoryEventArgs(item));
        }

    }

    internal void UseItem(IInventoryItem item)
    {
        if (items.Contains(item))
        {
            item.OnUse();
            ItemUsed?.Invoke(this, new InventoryEventArgs(item));
        }
    }

    public IInventoryItem GetItem(string keybind)
    {
        foreach (IInventoryItem item in items)
        {
            if (item.Keybind.Equals(keybind))
            {
                return item;
            }
        }
        Debug.LogError("There is no item on this keybind");
        return null;
    }
}