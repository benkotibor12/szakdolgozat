using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private Inventory inventory;
    public Transform cameraTransform;
    public Vector3 equipmentOffset;  // Offset of the equipment from the camera
    public Vector3 equipmentRotation;  // Rotation of the equipment relative to the camera

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventory.ItemSelected += Inventory_ItemSelected;
    }

    void LateUpdate()
    {
        // Set the position and rotation of the equipment relative to the camera
        transform.position = cameraTransform.position + cameraTransform.forward * equipmentOffset.z + cameraTransform.up * equipmentOffset.y + cameraTransform.right * equipmentOffset.x;
        transform.rotation = cameraTransform.rotation * Quaternion.Euler(equipmentRotation);
    }

    private void Inventory_ItemSelected(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        GameObject itemAsGameObject = (item as MonoBehaviour).gameObject;
        itemAsGameObject.SetActive(true);
        itemAsGameObject.transform.SetParent(transform, true);
        itemAsGameObject.transform.localPosition = item.PickUpPosition;
        itemAsGameObject.transform.localEulerAngles = item.PickUpRotation;
        itemAsGameObject.transform.localScale = item.PickUpScale;

    }
}
