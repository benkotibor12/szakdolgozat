using UnityEngine;

public class PlayerLook : MonoBehaviour, IDataPersistence
{
    public Camera playerCamera;
    private float rotationX = 0f;
    public float rotationZ = 0;
    public float rotationY = 0;
    public float horizontalSensitivity = 30f;
    public float verticalSensitivity = 30f;


    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        rotationX -= (mouseY * Time.deltaTime) * horizontalSensitivity;
        rotationX = Mathf.Clamp(rotationX, -80.0f, 80.0f);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        transform.Rotate((mouseX * Time.deltaTime) * verticalSensitivity * Vector3.up);
    }

    public void LoadData(GameData gameData)
    {
        horizontalSensitivity = gameData.horizontalSensitivity;
        verticalSensitivity = gameData.verticalSensitivity;
    }

    public void SaveData(ref GameData gameData)
    {
        //
    }
}
