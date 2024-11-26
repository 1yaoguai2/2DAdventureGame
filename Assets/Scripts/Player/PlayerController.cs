using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions _inputControl;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private PhysicsCheck _physicsCheck;
    [Header("运动参数")] [SerializeField] Vector2 inputDirection;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    public float hurtForce;
    public bool isHurt;
    public bool isDeath;
    public bool isAttack;

    public UnityEvent playerAttackEvent;
    private void Awake()
    {
        _inputControl = new InputSystem_Actions();
        _inputControl.GamePlay.Jump.started += PlayerJump;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _physicsCheck = GetComponent<PhysicsCheck>();

        //攻击
        _inputControl.GamePlay.Attack.started += PlayerAttack;
    }

    private void OnEnable()
    {
        _inputControl.Enable();
    }

    private void OnDisable()
    {
        _inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = _inputControl.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
            PlayerMove();
    }

    private void PlayerMove()
    {
        _rb.linearVelocityX = inputDirection.x * moveSpeed * Time.deltaTime;
        //角色朝向
        if (inputDirection.x > 0)
            _spriteRenderer.flipX = false;
        else if (inputDirection.x < 0)
            _spriteRenderer.flipX = true;
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        //Debug.Log("跳跃按钮");
        if (_physicsCheck.isGround)
            _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }


    /// <summary>
    /// 角色攻击
    /// </summary>
    /// <param name="context"></param>
    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAttackEvent?.Invoke();
        isAttack = true;
        _rb.linearVelocityX = 0;
    }

    #region UnityEvent -> CharacterScript
    /// <summary>
    /// 受伤
    /// Character脚本事件绑定
    /// </summary>
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        _rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }


    /// <summary>
    /// 接受死亡
    /// </summary>
    public void GetDeath()
    {
        isDeath = true;
        _inputControl.GamePlay.Disable();
    }

    #endregion
}