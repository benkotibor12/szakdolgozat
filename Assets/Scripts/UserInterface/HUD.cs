using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public Inventory Inventory;

    private void Start()
    {
        Inventory.ItemAdded += Inventory_ItemAdded;
    }

    private void Inventory_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach(Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            string keybind = slot.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text;
            if (!image.enabled && e.Item.Keybind.Equals(keybind))
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                break;
            }
        }
    }
}
