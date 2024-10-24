using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Sam Robichaud
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager


    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public Vector2 movementInput;
    public float moveAmount;

    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    public bool isPauseKeyPressed = false;

    public void Look(InputAction.CallbackContext context)
    {

            // Get mouse input for the camera
            cameraInput = context.ReadValue<Vector2>();

            // Get scroll input for camera zoom
            scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Send inputs to CameraManager
            cameraManager.zoomInput = scrollInput;
            cameraManager.cameraInput = cameraInput;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
            isPauseKeyPressed = true;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if(context.performed)
            playerLocomotionHandler.isSprinting = true;
        if(context.canceled)
            playerLocomotionHandler.isSprinting = false;
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
            playerLocomotionHandler.HandleJump(); // Trigger jump in locomotion handler
    }
}
