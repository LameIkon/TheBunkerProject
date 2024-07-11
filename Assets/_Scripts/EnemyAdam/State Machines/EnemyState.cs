public class EnemyState
{
    protected Enemy _enemy;
    protected EnemyStateMachine _enemyStateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine enemyStateMachine)
    {
        enemy = _enemy;
        enemyStateMachine = _enemyStateMachine;
    }
    
    public virtual void EnterState() { }
    
    public virtual void ExitState() { }
    
    public virtual void FrameUpdate() { }
    
    public virtual void PhysicsUpdate() { }
    
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType) { }
}
