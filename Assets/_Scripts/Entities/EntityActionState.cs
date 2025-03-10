// Global access to check what type of state an entity is currently in. Used by Player and Smart Intelligence (when implemented in ai)
public enum EntityActionState
{
    Idle,
    Walking,
    Running,
    CrouchingIdle,
    CrouchingWalking,
    WalkingBackwards
}