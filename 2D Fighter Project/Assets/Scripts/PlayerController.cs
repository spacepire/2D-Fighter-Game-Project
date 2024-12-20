using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;


    private Rigidbody2D playerRigidbody;
    PlayerInputActions playerInputActions;

    //Props
    public Vector2 _inputVector { get; private set; }
    public bool _isAttack { get; private set; }

    //Events & Delegates
    public static PlayerController Instance { get; private set; }

    public static Action OnAttackAnimation;

    private void Awake()
    {
        _isAttack = false;
        Instance = this;

        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump_Performend;
        playerInputActions.Player.Attacks.started += Attacks_Performed;
        playerInputActions.Player.Attacks.performed += Attacks_Performed;
        playerInputActions.Player.Attacks.canceled += Attacks_Performed;
    }
    
    private void OnDestroy()
    {
        playerInputActions.Player.Jump.performed -= Jump_Performend;
        playerInputActions.Player.Attacks.started -= Attacks_Performed;
        playerInputActions.Player.Disable();

    }

    private void FixedUpdate()
    {
            Movement();
    }

    private void Movement()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        _inputVector = inputVector;
        PlayerRotation();
        playerRigidbody.linearVelocityX = inputVector.x * speed * Time.fixedDeltaTime;
    }

    private void PlayerRotation()
    {
        if (_inputVector.x == 1f)
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (_inputVector.x == -1f)
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void Jump_Performend(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            playerRigidbody.linearVelocityY = jumpPower * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Attacks_Performed(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);

        if (context.phase == InputActionPhase.Started)
        {
            _isAttack = true;
            Debug.Log("Attack started!");
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            if (_isAttack)
            {
                OnAttackAnimation?.Invoke();
                Debug.Log("Attack performed!");
            }
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _isAttack = false;
            Debug.Log("Attack canceled!");
        }

    }
}
