using UnityEditor;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("检测参数")]
    public float checkRadius;
    public Vector2 bottomDffset;
    public LayerMask groundLayer;
    [Header("状态")]
    public bool isGround;


    private void Update()
    {
        //检测地面
        isGround = Physics2D.OverlapCircle(transform.position, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomDffset, checkRadius);
    }
}
