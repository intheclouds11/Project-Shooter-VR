using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputActionAsset XRControls;
    private InputAction jump;
    [SerializeField] float jumpHeight = 1.0f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private CharacterController controller;
    private float gravityValue = -9.81f;

    private void OnEnable()
    {
        EnableControls();
    }

    private void OnDisable()
    {
        DisableControls();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void EnableControls()
    {
        var gameplayActionMapRH = XRControls.FindActionMap("XRI RightHand");

        jump = gameplayActionMapRH.FindAction("Jump");
        jump.performed += ProcessJump;
        jump.Enable();
    }

    private void DisableControls()
    {
        jump.performed -= ProcessJump;
        jump.Disable();
    }

    void ProcessJump(InputAction.CallbackContext context)
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (jump.IsPressed() && groundedPlayer)
        {
            Debug.Log("Jump pressed!");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        
    }
    
}
