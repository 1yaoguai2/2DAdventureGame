using UnityEngine;

public class PlayerAnimtions : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void FixedUpdate()
    {
        SetAnimations();
    }

    void SetAnimations()
    {
        animator.SetFloat("speed",Mathf.Abs(rb.linearVelocityX));
        animator.SetFloat("jumpSpeed",Mathf.Abs(rb.linearVelocityY));
        animator.SetBool("isGround",physicsCheck.isGround);
    }

    public void SetHurtTrigger()
    {
        animator.SetTrigger("isHurt");
    }
}
