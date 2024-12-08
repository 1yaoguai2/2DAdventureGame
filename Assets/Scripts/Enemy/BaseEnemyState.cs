
public abstract class BaseEnemyState
{
    public BaseEnemy currentEmeny;
    public abstract void OnEnter(BaseEnemy enemy);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();

}
