using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera playerCamera;
    private float rotationX = 0f;
    public float rotationZ = 0;
    public float rotationY = 0;
    public float sensitivityX = 30f;
    public float sensitivityY = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        rotationX -= (mouseY * Time.deltaTime) * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        transform.Rotate((mouseX * Time.deltaTime) * sensitivityX * Vector3.up);
    }
}
