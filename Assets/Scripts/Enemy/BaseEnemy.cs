using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseEnemy : MonoBehaviour
{
    private Rigidbody2D _rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck physicsCheck;
    [Header("基础参数")] [SerializeField] public float patrolSpeed;
    [SerializeField] public float chaseSpeed;

    public float currentSpeed;
    public float faceDir = 1f;
    public float hurtForce;

    public Transform attackerTrans;

    [Header("检测")] public Vector2 centerOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    [Header("计时器")] public float waitTime;
    public float waitTimeCenter;
    public float lostTime;
    public float lostTimeCenter;

    [Header("状态")] public bool isWait;
    public bool isLost;
    public bool isHurt;
    public bool isDead;
    public BaseEnemyState currentState;
    public BaseEnemyState patrolState;
    public BaseEnemyState chaseState;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = patrolSpeed;
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

    /// <summary>
    /// 移动
    /// </summary>
    protected virtual void Move()
    {
        if (!isHurt && !isDead && !isWait)
        {
            _rb.linearVelocityX = currentSpeed * faceDir * Time.deltaTime;
        }
        // else
        // {
        //     _rb.linearVelocityX = 0;
        //     CustomLogger.Log("isWait:"+_isWait);
        // }
    }


    /// <summary>
    /// 时间
    /// </summary>
    private void TimeCounter()
    {
        if (isWait)
        {
            waitTimeCenter += Time.deltaTime;
            if (waitTimeCenter > waitTime)
            {
                waitTimeCenter = 0;
                isWait = false;
                transform.localScale = new Vector3(faceDir, 1, 1);
                anim.SetBool("Walk", true);
            }
        }


        if (FoundPlayer())
        {
            lostTimeCenter = lostTime;
            isLost = false;
        }
        else
        {
            if (!isLost)
            {
                lostTimeCenter -= Time.deltaTime;
                if (lostTimeCenter < 0)
                    isLost = true;
            }
        }
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="attackTrans"></param>
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
        if (isWait)
        {
            isWait = false;
            waitTimeCenter = 0;
        }

        Stop();
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


    /// <summary>
    /// 发现敌人
    /// </summary>
    /// <returns></returns>
    public bool FoundPlayer()
    {
        var find = Physics2D.BoxCast((Vector2)transform.position + centerOffset, checkSize, 0, new Vector2(faceDir, 0),
            checkDistance, attackLayer);
        return find;
    }


    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="state"></param>
    public void CutState(NPCState state)
    {
        var newState = state switch
        {
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState?.OnEnter(this);
        CustomLogger.Log("切换状态：" + state);
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        _rb.linearVelocityX = 0;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(faceDir * checkDistance, 0, 0),
            0.2f);
    }
}