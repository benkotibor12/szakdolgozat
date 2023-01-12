using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera camera;
    private float rotationX = 0f;

    public float sensitivityX = 30f;
    public float sensitivityY = 30f;
   
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        rotationX -= (mouseY * Time.deltaTime) * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);
        camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * sensitivityX);
    }
}
