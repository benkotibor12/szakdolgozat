using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Inventory inventory;
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    PlayerUI playerUI;
    private InputManager inputManager;

    void Start()
    {
        cam = GetComponent<PlayerLook>().camera;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, distance, mask))
        {
            if (raycastHit.collider.GetComponent<Interactable>() != null && raycastHit.collider.GetComponent<IInventoryItem>() != null)
            {
                Interactable interactable = raycastHit.collider.GetComponent<Interactable>();
                IInventoryItem item = raycastHit.collider.GetComponent<IInventoryItem>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteraction();
                    inventory.AddItem(item);
                }
            }
            else if (raycastHit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = raycastHit.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteraction();
                }
            }
        }
    }
}
