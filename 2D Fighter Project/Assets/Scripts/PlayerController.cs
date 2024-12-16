using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.Player.Move.performed += MoveController;
    }/*
    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        float speed = 5;

        playerRigidbody.AddForce(new Vector2(inputVector.x, inputVector.y) * speed);
    }

    */
    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Çalışıyor" + context);
        if (context.performed)
        {
            playerRigidbody.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }





    
    private void MoveController(InputAction.CallbackContext context)
    {
        Debug.Log("Çalışıyor" + context);

        Vector2 inputVector = context.ReadValue<Vector2>();

        float speed = 5f;

        playerRigidbody.AddForce(new Vector3(inputVector.x, 0, 0) * speed * Time.fixedDeltaTime, ForceMode2D.Force);
        
    }
}
