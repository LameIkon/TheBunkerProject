public interface IMovable
{
    void Move(float movementX);
    void Sprint(bool isSprinting);
    void Crouch(bool isCrouching);
    void Climb(bool isClimbing);
}
