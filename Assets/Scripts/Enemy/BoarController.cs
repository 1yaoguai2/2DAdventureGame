

public class BoarController : BaseEnemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new BoarPatrolState();
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