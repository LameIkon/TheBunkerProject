public interface IMovable
{
    void Move(float movementX, float movementY);
    void Sprint(bool isSprinting);
    void Crouch(bool isCrouching);
    void Climb(bool isClimbing);
}
