public class EnemyStateMachine
{
   public EnemyState _CurrentEnemyState { get; set; }

   public void Initialize(EnemyState startingState)
   {
      _CurrentEnemyState = startingState;
      _CurrentEnemyState.EnterState();
   }

   public void ChangeState(EnemyState newState)
   {
      _CurrentEnemyState.ExitState();
      _CurrentEnemyState = newState;
      _CurrentEnemyState.EnterState();
   }
}
