public class BoarPatrolState : BaseEnemyState
{
    public override void OnEnter(BaseEnemy enemy)
    {
        currentEmeny = enemy;
    }

    public override void LogicUpdate()
    {
        //TODO:发现player，追击
        //巡逻掉头
        if(currentEmeny._isWait) return;
        if (!currentEmeny.physicsCheck.isGround ||
            (currentEmeny.physicsCheck.isTouchLeftWall && currentEmeny.faceDir < 0) ||
            (currentEmeny.physicsCheck.isTouchRightWall && currentEmeny.faceDir > 0))
        {
            currentEmeny._isWait = true;
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
        currentEmeny.anim.SetBool("Walk",false);
    }
}