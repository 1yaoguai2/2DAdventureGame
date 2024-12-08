using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseEnemy : MonoBehaviour
{
    private Rigidbody2D _rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [Header("基础参数")] [SerializeField] private float normalSpeed;
    [SerializeField] private float chaseSpeed;

    public float currentSpeed;
    public float faceDir;
    public float hurtForce;

    public Transform attackerTrans;

    [FormerlySerializedAs("waiteTime")] [Header("计时器")]
    public float waitTime;

    public float waitTimeCenter;
    [HideInInspector] public bool _isWait;

    [Header("状态")] public bool isHurt;
    public bool isDead;

    public BaseEnemyState currentState;
    public BaseEnemyState patrolState;
    public BaseEnemyState attackState;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void FixedUpdate()
    {
        Move();
        TimeCounter();

        //状态
        currentState.PhysicsUpdate();
    }

    private void Update()
    {
        faceDir = -transform.localScale.x;
        currentState.LogicUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    protected virtual void Move()
    {
        if (!isHurt && !isDead && !_isWait)
        {
            _rb.linearVelocityX = currentSpeed * faceDir * Time.deltaTime;
        }
        // else
        // {
        //     _rb.linearVelocityX = 0;
        //     LogManager.Log("isWait:"+_isWait);
        // }
    }


    private void TimeCounter()
    {
        if (_isWait)
        {
            waitTimeCenter += Time.deltaTime;
            if (waitTimeCenter > waitTime)
            {
                waitTimeCenter = 0;
                _isWait = false;
                transform.localScale = new Vector3(faceDir, 1, 1);
                anim.SetBool("Walk", true);
            }
        }
    }

    public void OnTakeDamage(Transform attackTrans)
    {
        attackerTrans = attackTrans;
        //转身
        if (attackerTrans.position.x - transform.position.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (attackerTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        
        //受伤
        isHurt = true;
        anim.SetTrigger("Hurt");
        //墙边受到攻击，结束掉头逻辑，清空计时
        if (_isWait)
        {
            _isWait = false;
            waitTimeCenter = 0;
        }

        StartCoroutine(AddHurt());
    }

    /// <summary>
    /// 添加力，力消失后，取消受伤状态
    /// </summary>
    /// <returns></returns>
    private IEnumerator AddHurt()
    {
        //击退
        Vector2 dir = new Vector2(transform.position.x - attackerTrans.position.x, 0).normalized;
        _rb.AddForce(hurtForce * dir, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        isHurt = false;
    }


    /// <summary>
    /// 设置死亡
    /// </summary>
    public void SetDead()
    {
        anim.SetBool("Dead", true);
        isDead = true;
        StartCoroutine(AddHurt());
        Destroy(this.gameObject, 1.1f);
    }
}