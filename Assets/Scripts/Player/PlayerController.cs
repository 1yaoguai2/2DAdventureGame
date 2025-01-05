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

    [Header("监听事件")] public VoidEventSO restGameEventSo;

    public VoidEventSO loadSceneStartEventSo;

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
        restGameEventSo.OnEventRaised += RestGameEvent;
        loadSceneStartEventSo.OnEventRaised += LoadSceneStartEvent;
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
        //角色朝向,Flip只翻转人物贴图，不反转攻击特殊物体
        // if (inputDirection.x > 0)
        //     _spriteRenderer.flipX = false;
        // else if (inputDirection.x < 0)
        //     _spriteRenderer.flipX = true;
        if (inputDirection.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (inputDirection.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
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
        if (!_physicsCheck.isGround) return;
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

    //重新开始
    private void RestGameEvent()
    {
        _inputControl.GamePlay.Enable();
    }

    private void LoadSceneStartEvent()
    {
        isDeath = false;
        _inputControl.GamePlay.Enable();
        var _animator = transform.GetComponent<Animator>();
        _animator.SetBool("isDeath", false);
        _animator.SetFloat("speed", 1f);
    }

    #endregion
}