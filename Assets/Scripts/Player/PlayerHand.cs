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
        itemAsGameObject.transform.parent = transform;
        itemAsGameObject.transform.position = transform.position;
    }
}
