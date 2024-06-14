using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    public void MoveByRigidbodyVelocity(Rigidbody2D rb, Vector2 vector)
    {
        Vector2 nextPosition = rb.position + vector * this.speed;
        rb.MovePosition(nextPosition);
    }
}