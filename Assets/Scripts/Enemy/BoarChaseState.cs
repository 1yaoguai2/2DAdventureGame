using UnityEngine;

public class BoarChaseState : BaseEnemyState
{
    public override void OnEnter(BaseEnemy enemy)
    {
        currentEmeny = enemy;
        currentEmeny.currentSpeed = currentEmeny.chaseSpeed;
        currentEmeny.anim.SetBool("Run", true);
    }

    public override void LogicUpdate()
    {
        if (!currentEmeny.physicsCheck.isGround ||
            (currentEmeny.physicsCheck.isTouchLeftWall && currentEmeny.faceDir < 0) ||
            (currentEmeny.physicsCheck.isTouchRightWall && currentEmeny.faceDir > 0))
        {
            currentEmeny.transform.localScale = new Vector3(currentEmeny.faceDir, 1, 1);
        }

        if (!currentEmeny.FoundPlayer())
        {
            if (currentEmeny.isLost)
                currentEmeny.CutState(NPCState.Patrol);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        currentEmeny.anim.SetBool("Run", false);
    }
}