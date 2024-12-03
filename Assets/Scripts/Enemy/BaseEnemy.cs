using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseEnemy : MonoBehaviour
{
    private Rigidbody2D _rb;
    protected Animator anim;
    protected PhysicsCheck physicsCheck;
    [Header("基础参数")] [SerializeField] private float normalSpeed;
    [SerializeField] private float chaseSpeed;

    public float currentSpeed;
    public float faceDir;

    [FormerlySerializedAs("waiteTime")] [Header("计时器")]
    public float waitTime;

    public float waitTimeCenter;
    private bool _isWait;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }

    private void FixedUpdate()
    {
        faceDir = -transform.localScale.x;
        Move();
        Touch();
        TimeCounter();
    }

    protected virtual void Move()
    {
        _rb.linearVelocityX = currentSpeed * faceDir * Time.deltaTime;
    }

    private void Touch()
    {
        if (_isWait) return;
        if ((physicsCheck.isTouchLeftWall && faceDir < 0) || (physicsCheck.isTouchRightWall && faceDir > 0))
        {
            _isWait = true;
            anim.SetBool("Walk", false);
        }
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
}