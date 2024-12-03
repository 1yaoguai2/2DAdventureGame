using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BoarController : BaseEnemy
{
    private void Start()
    {
        anim.SetBool("Walk",true);
    }

    protected override void Move()
    {
        base.Move();
    }
}