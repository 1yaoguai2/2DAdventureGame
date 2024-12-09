

public class BoarController : BaseEnemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
    }

    void Start()
    {
        LogManager.Log(name);
    }

    protected override void Move()
    {
        base.Move();
    }
}