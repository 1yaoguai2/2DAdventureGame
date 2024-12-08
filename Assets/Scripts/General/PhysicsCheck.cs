using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    //手动
    public bool isManual;

    public float checkRadius;
    private CapsuleCollider2D _capsuleCollider2D;

    //墙壁
    public Vector2 leftOffset;
    public Vector2 rightOffset;

    //地面
    public Vector2 bottomOffset;
    public LayerMask groundLayer;
    [Header("状态")] public bool isGround;

    public bool isTouchLeftWall;
    public bool isTouchRightWall;

    private Vector2 Pos2D => (Vector2)transform.position;

    private void Awake()
    {
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        if (!isManual)
        {
            rightOffset = new Vector2((_capsuleCollider2D.bounds.size.x + _capsuleCollider2D.offset.x) / 2f,
                _capsuleCollider2D.bounds.size.y / 2f);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }

    private void Update()
    {
        //检测地面
        if (transform.localScale.x > 0)
            isGround = Physics2D.OverlapCircle(Pos2D + bottomOffset, checkRadius, groundLayer);
        else
            isGround = Physics2D.OverlapCircle(Pos2D - bottomOffset, checkRadius, groundLayer);
        //检测墙壁
        isTouchRightWall = Physics2D.OverlapCircle(Pos2D + rightOffset, checkRadius, groundLayer);
        isTouchLeftWall = Physics2D.OverlapCircle(Pos2D + leftOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Pos2D + bottomOffset, checkRadius);
        Gizmos.DrawWireSphere(Pos2D + rightOffset, checkRadius);
        Gizmos.DrawWireSphere(Pos2D + leftOffset, checkRadius);
    }
}