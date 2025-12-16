using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float SpeedMoving = 5.0f;
    public Vector2 DirectionMoving = new Vector2(0, -1f);
    private bool collisionWithPlayer = false;

    private Rigidbody2D rb2d;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Launch()
    {
        rb2d.linearVelocity = DirectionMoving.normalized * SpeedMoving;
    }

    private void Start()
    {
        //Launch();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GetIsGameRunning() == true) {
            Vector2 dir = rb2d.linearVelocity.normalized;

            float minAngleDegrees = 5f;

            float angleToHorizontal = Vector2.Angle(dir, Vector2.right); 
            if (angleToHorizontal < minAngleDegrees || angleToHorizontal > 180 - minAngleDegrees) {
                float angleRad = minAngleDegrees * Mathf.Deg2Rad;
                float y = Mathf.Sin(angleRad);
                float x = Mathf.Cos(angleRad) * Mathf.Sign(dir.x);
                
                dir = new Vector2(x, y*(collisionWithPlayer ? 1 : -1));
            }

            rb2d.linearVelocity = dir.normalized * SpeedMoving;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("TopWall") || other.gameObject.CompareTag("Wall")) {
            SoundManager.Instance.Play("BounceBall");
        }
    }
}
