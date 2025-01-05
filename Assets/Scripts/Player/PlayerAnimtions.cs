using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimtions : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private PhysicsCheck _physicsCheck;
    private PlayerController _playerController;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        _playerController = GetComponent<PlayerController>();
    }



    private void FixedUpdate()
    {
        SetAnimations();
    }

    void SetAnimations()
    {
        _animator.SetFloat("speed",Mathf.Abs(_rb.linearVelocityX));
        _animator.SetFloat("jumpSpeed",Mathf.Abs(_rb.linearVelocityY));
        _animator.SetBool("isGround",_physicsCheck.isGround);
        _animator.SetBool("isDeath",_playerController.isDeath);
        _animator.SetBool("isAttack",_playerController.isAttack);
    }

    public void SetHurtTrigger()
    {
        _animator.SetTrigger("isHurt");
    }

    public void SetAttackTrigger()
    {
        _animator.SetTrigger("attack");
    }
    
}
