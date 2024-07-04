using UnityEngine;

public abstract class MoveComponent
{
    public void Move(Rigidbody2D rb, Vector2 vector, float speed)
    {
        Vector2 nextPosition = rb.position + vector * speed;
        rb.MovePosition(nextPosition);
    }
}