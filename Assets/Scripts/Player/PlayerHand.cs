using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventory.ItemSelected += Inventory_ItemSelected;
    }

    private void Inventory_ItemSelected(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        GameObject itemAsGameObject = (item as MonoBehaviour).gameObject;
        itemAsGameObject.SetActive(true);
        itemAsGameObject.transform.SetParent(transform, true);
        itemAsGameObject.transform.localPosition = (item as Equipment).PickUpPosition;
        itemAsGameObject.transform.localEulerAngles = (item as Equipment).PickUpRotation;
        itemAsGameObject.transform.localScale = (item as Equipment).PickUpScale;

    }
}
