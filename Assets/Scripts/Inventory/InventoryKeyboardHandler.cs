using UnityEngine;

public class InventoryKeyboardHandler : MonoBehaviour
{
    private Inventory inventory;
    private InputManager inputManager;
    private IInventoryItem item;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (inputManager.onFoot.Inventory.triggered)
        {
            string input = inputManager.onFoot.Inventory.activeControl.name;
            if (int.TryParse(input, out int inputNum) && inputNum >= 1 && inputNum <= 8)
            {
                Debug.Log(inputNum);
                item = inventory.GetItem(input);
                inventory.SelectItem(item);
            }

        }
        if (inputManager.onFoot.Use.triggered)
        {
            if (item != null)
            {
                inventory.UseItem(item);
            }
        }
    }
}