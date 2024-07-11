using UnityEngine;

public interface IEnemyMoveable
{
    Rigidbody2D _RB { get; set; }
    
    bool _IsFacingRight { get; set; }

    void MoveEnemy(Vector2 velocity);

    void CheckForLeftOrRightFacing(Vector2 velocity);
}
