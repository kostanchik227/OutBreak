using UnityEngine;

public class CustomBounce : MonoBehaviour
{
    private BoxCollider2D _objectCollider;
    public float XMultiplier = 1.5f;
    private bool IsCollision = false;
    public GameObject CollisionObject;

    private void Awake()
    {
        _objectCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (IsCollision && CollisionObject != null) {
            float DistX = CollisionObject.transform.position.x - transform.position.x;
            float DistY = CollisionObject.transform.position.y - transform.position.y;
            BallMovement ball = CollisionObject.GetComponent<BallMovement>();
            ball.DirectionMoving = new Vector2(DistX * XMultiplier, DistY).normalized;
            ball.Launch();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            IsCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball")) {
            IsCollision = false;
        }
    }
}
