using UnityEngine;

public class LaserBeamMovement : MonoBehaviour {
    [Header("Settings")]
    public float travelSpeed = 30f;

    private Rigidbody2D rb;
    public void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        rb.simulated = false;
    }

    public void RunLaser()
    {
        rb.simulated = true;

        Vector2 direction = transform.right;
        rb.linearVelocity = direction * travelSpeed;
    }

    void Update()
    {
    }
}