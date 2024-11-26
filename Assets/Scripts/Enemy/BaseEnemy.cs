using System;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    [Header("基础参数")] [SerializeField] private float normalSpeed;
    [SerializeField] private float chaseSpeed;

    public float currentSpeed;
    public float faceDir;

    private  void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        currentSpeed = normalSpeed;
    }

    private void FixedUpdate()
    {
        faceDir = transform.localScale.x;
        Move();
    }

    private void Move()
    {
        _rb.linearVelocityX = currentSpeed * faceDir * Time.deltaTime * -1f;
    }
}