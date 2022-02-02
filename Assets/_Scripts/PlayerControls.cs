using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionAsset XRControls;
    private InputAction _jump;
    [SerializeField] private float jumpHeight = 1.0f;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private CharacterController _controller;
    private float _gravityValue = -9.81f;

    private void OnEnable()
    {
        EnableJump();
    }

    private void OnDisable()
    {
        DisableJump();
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _playerVelocity.y += _gravityValue * Time.deltaTime;

        _controller.Move(_playerVelocity * Time.deltaTime); // for jump
    }

    private void EnableJump()
    {
        var gameplayActionMapRH = XRControls.FindActionMap("XRI RightHand");

        _jump = gameplayActionMapRH.FindAction("Jump");
        _jump.performed += ProcessJump;
        _jump.Enable();
    }

    private void DisableJump()
    {
        _jump.performed -= ProcessJump;
        _jump.Disable();
    }

    private void ProcessJump(InputAction.CallbackContext context)
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        if (_jump.IsPressed() && _groundedPlayer)
        {
            Debug.Log("Jump pressed!");
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * _gravityValue);
        }
    }
}