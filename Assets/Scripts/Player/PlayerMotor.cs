using System.Collections;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Animator playerAnimator;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        StartCoroutine(WaitForMapGeneration());
    }

    IEnumerator WaitForMapGeneration()
    {
        yield return new WaitUntil(() => GameManager.Instance.GetInitializedCells().Count > 0);
        transform.position = GetSpawnPoint();
    }


    void Update()
    {
        isGrounded = controller.isGrounded;
        playerAnimator.SetFloat("jumpVelocity", playerVelocity.y);
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -3f;
            playerAnimator.SetBool("isJumping", false);

        }
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        playerAnimator.SetBool("isJumping", true);
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    private Vector3 GetSpawnPoint()
    {
        return GameManager.Instance.GetInitializedCells()[0].transform.position;
    }
}
