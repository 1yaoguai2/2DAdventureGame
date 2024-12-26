
public class BoarPatrolState : BaseEnemyState
{

    public override void OnEnter(BaseEnemy enemy)
    {
        currentEmeny = enemy;
        currentEmeny.currentSpeed = currentEmeny.patrolSpeed;
        currentEmeny.anim.SetBool("Walk", true);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void LogicUpdate()
    {
        //TO-DO:发现player，追击
        if (currentEmeny.FoundPlayer())
        {
            currentEmeny.CutState(NPCState.Chase);
        }
               

        //巡逻掉头
        if (currentEmeny.isWait) return;
        if (!currentEmeny.physicsCheck.isGround ||
            (currentEmeny.physicsCheck.isTouchLeftWall && currentEmeny.faceDir < 0) ||
            (currentEmeny.physicsCheck.isTouchRightWall && currentEmeny.faceDir > 0))
        {
            currentEmeny.Stop();
            currentEmeny.isWait = true;
            currentEmeny.anim.SetBool("Walk", false);
        }
        else
        {
            currentEmeny.anim.SetBool("Walk", true);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        currentEmeny.anim.SetBool("Walk", false);
    }
}