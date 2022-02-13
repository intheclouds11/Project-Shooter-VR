using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionAsset XRControls;
    private InputActionMap _gameplayActionMapRH;
    private InputAction _jump;
    [SerializeField] private float jumpHeight = 1.0f;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private CharacterController _controller;
    private float _gravityValue = -9.81f;
    private float _jumpTime;
    private bool _isJumping;
    [SerializeField] private float tinyHopTime = 0.1f;
    [SerializeField] private float smallHopTime = 0.3f;
    [SerializeField] private float mediumHopTime = 0.6f;
    [SerializeField] private float tinyHopDeceleration = 30f;
    [SerializeField] private float smallHopDeceleration = 20f;
    [SerializeField] private float mediumHopDeceleration = 15f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _gameplayActionMapRH = XRControls.FindActionMap("XRI RightHand");
        _jump = _gameplayActionMapRH.FindAction("Jump");
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
        _jump.performed += ProcessJump;
        _jump.canceled += CancelJump;
        _jump.Enable();
    }

    private void DisableJump()
    {
        _jump.performed -= ProcessJump;
        _jump.canceled -= CancelJump;
        _jump.Disable();
    }

    private void Update()
    {
        _playerVelocity.y += _gravityValue * Time.deltaTime;

        _controller.Move(_playerVelocity * Time.deltaTime); // for jump only currently

        CalculateJumpTime();
    }

    private void CalculateJumpTime()
    {
        if (_isJumping)
        {
            _jumpTime += Time.deltaTime;
        }
    }

    private void ProcessJump(InputAction.CallbackContext obj)
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        if (_jump.IsPressed() && _groundedPlayer)
        {
            _isJumping = true;
            Debug.Log("Jump pressed!");
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * _gravityValue);
        }
    }

    private void CancelJump(InputAction.CallbackContext obj)
    {
        _isJumping = false;
        if (_playerVelocity.y < 0)
        {
            _jumpTime = 0f;
            return;
        }

        if (_jumpTime < tinyHopTime)
        {
            Debug.Log("tiny hop");
            _playerVelocity.y += _gravityValue / (_jumpTime * tinyHopDeceleration);
        }
        else if (_jumpTime > tinyHopTime && _jumpTime < smallHopTime)
        {
            Debug.Log("small hop");
            _playerVelocity.y += _gravityValue / (_jumpTime * smallHopDeceleration);
        }
        else if (_jumpTime > smallHopTime && _jumpTime < mediumHopTime)
        {
            Debug.Log("medium hop");
            _playerVelocity.y += _gravityValue / (_jumpTime * mediumHopDeceleration);
        }

        _jumpTime = 0f;
    }
}