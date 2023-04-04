using UnityEngine;

public class Flashlight : Interactable, IInventoryItem
{
    [SerializeField] private string itemName;
    [SerializeField] private string keybind;
    [SerializeField] private Sprite image;
    [SerializeField] private bool inUse;

    public Vector3 PickUpPosition;
    public Vector3 PickUpRotation;
    public Vector3 PickUpScale;

    public string Name => itemName;
    public Sprite Image => image;
    public string Keybind => keybind;
    public bool InUse => inUse;

    Vector3 IInventoryItem.PickUpPosition => PickUpPosition;
    Vector3 IInventoryItem.PickUpRotation => PickUpRotation;
    Vector3 IInventoryItem.PickUpScale => PickUpScale;

    Light lightComponent;
    public float currentCharge = 100f;
    public float maxCharge = 100f;
    public float powerConsumption = 0.1f;
    public float maxIntensity;

    private void Start()
    {
        lightComponent = transform.Find("Light").GetComponent<Light>();
        maxIntensity = lightComponent.intensity;
    }

    private void Update()
    {
        if (inUse)
        {
            currentCharge -= Time.deltaTime * powerConsumption;
            float lightIntensity = currentCharge / maxCharge;
            lightComponent.intensity = lightIntensity * maxIntensity;
        }
    }

    public void OnPickup()
    {
        //gameObject.SetActive(false);
        gameObject.layer = 0;
        inUse = false;
    }

    public void OnSelect()
    {
        //sound, animation
    }

    public void OnUse()
    {
        inUse = !inUse;
        transform.Find("Light").gameObject.SetActive(inUse);
    }

    protected override void Interact()
    {
        promptMessage.Equals("Collect");
    }

    public void ChargeBattery(float charge)
    {
        currentCharge = Mathf.Min(maxCharge, currentCharge + charge);
    }
}
