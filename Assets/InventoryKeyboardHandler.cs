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
            switch (input)
            {
                case "1":
                    Debug.Log("1");
                    item = inventory.GetItem(input);
                    inventory.SelectItem(item);
                    break;
                case "2":
                    Debug.Log("2");
                    item = inventory.GetItem(input);
                    inventory.SelectItem(item);
                    break;
                case "3":
                    Debug.Log("3");
                    item = inventory.GetItem(input);
                    inventory.SelectItem(item);
                    break;
                case "4":
                    Debug.Log("4");
                    item = inventory.GetItem(input);
                    inventory.SelectItem(item);
                    break;
                case "5":
                    Debug.Log("5");
                    item = inventory.GetItem(input);
                    inventory.SelectItem(item);
                    break;
                case "6":
                    Debug.Log("6");
                    item = inventory.GetItem(input);
                    inventory.SelectItem(item);
                    break;
                case "7":
                    Debug.Log("7");
                    item = inventory.GetItem(input);
                    inventory.SelectItem(item);
                    break;
                case "8":
                    Debug.Log("8");
                    item = inventory.GetItem(input);
                    inventory.SelectItem(item);
                    break;

                default:
                    break;
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