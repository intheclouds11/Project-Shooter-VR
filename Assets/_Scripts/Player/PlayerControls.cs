using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionAsset XRControls;
    private InputActionMap gameplayActionMapRh;
    private InputAction jump;
    [SerializeField] private float jumpHeight = 5.0f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private CharacterController controller;
    private float gravityValue = -9.81f;
    private float jumpTime;
    private bool isJumping;
    [SerializeField] private float tinyHopTime = 0.1f;
    [SerializeField] private float smallHopTime = 0.3f;
    [SerializeField] private float mediumHopTime = 0.6f;
    [SerializeField] private float tinyHopDeceleration = 30f;
    [SerializeField] private float smallHopDeceleration = 20f;
    [SerializeField] private float mediumHopDeceleration = 15f;

    private Rigidbody rb;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        gameplayActionMapRh = XRControls.FindActionMap("XRI RightHand");
        jump = gameplayActionMapRh.FindAction("Jump");
    }

    private void OnEnable()
    {
        EnableJump();
    }

    private void OnDisable()
    {
        DisableJump();
    }

    private void EnableJump()
    {
        jump.performed += ProcessJump;
        jump.canceled += CancelJump;
        jump.Enable();
    }

    private void DisableJump()
    {
        jump.performed -= ProcessJump;
        jump.canceled -= CancelJump;
        jump.Disable();
    }

    private void Update()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime); // for jump only currently

        CalculateJumpTime();
    }

    private void CalculateJumpTime()
    {
        if (isJumping)
        {
            jumpTime += Time.deltaTime;
        }
    }

    private void ProcessJump(InputAction.CallbackContext obj)
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (jump.IsPressed() && groundedPlayer)
        {
            isJumping = true;
            Debug.Log("Jump pressed!");
            
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    private void CancelJump(InputAction.CallbackContext obj)
    {
        isJumping = false;
        if (playerVelocity.y < 0)
        {
            jumpTime = 0f;
            return;
        }

        if (jumpTime < tinyHopTime)
        {
            Debug.Log("tiny hop");
            playerVelocity.y += gravityValue / (jumpTime * tinyHopDeceleration);
        }
        else if (jumpTime > tinyHopTime && jumpTime < smallHopTime)
        {
            Debug.Log("small hop");
            playerVelocity.y += gravityValue / (jumpTime * smallHopDeceleration);
        }
        else if (jumpTime > smallHopTime && jumpTime < mediumHopTime)
        {
            Debug.Log("medium hop");
            playerVelocity.y += gravityValue / (jumpTime * mediumHopDeceleration);
        }

        jumpTime = 0f;
    }
}