using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInput.OnFootActions onFoot;
    private PlayerInput playerInput;

    private PlayerMotor motor;
    private PlayerLook look;
    //private Animator playerAnimator;

    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        //playerAnimator = GetComponent<Animator>();
        onFoot.Jump.performed += ctx =>
        {
            motor.Jump();
            //playerAnimator.SetBool("isJumping", true);
        };
    }

    void Update()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());

        if (onFoot.Movement.IsPressed())
        {
            //playerAnimator.SetBool("isWalking", true);
        }
        if (!onFoot.Movement.IsPressed())
        {
            //playerAnimator.SetBool("isWalking", false);
        }        
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
