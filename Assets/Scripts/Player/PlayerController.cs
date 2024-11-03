using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class PlayerController : MonoBehaviour
{
    public InputSystem_Actions inputControl;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PhysicsCheck physicsCheck;
    [Header("运动参数")]
    [SerializeField] Vector2 inputDirection;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    private void Awake()
    {
        inputControl = new InputSystem_Actions();
        inputControl.GamePlay.Jump.started += PlayerJump;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        rb.linearVelocityX = inputDirection.x * moveSpeed * Time.deltaTime;
        //角色朝向
        if (inputDirection.x > 0)
            spriteRenderer.flipX = false;
        else if (inputDirection.x < 0)
            spriteRenderer.flipX = true;
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        //Debug.Log("跳跃按钮");
        if (physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

}
