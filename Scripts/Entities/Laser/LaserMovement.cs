using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void UpdateVelocity()
    {
        rb2d.linearVelocity = transform.right * speed;
    }

    public float Speed
    {
        get => speed;
        set
        {
            speed = value;
            UpdateVelocity();
        }
    }

    void Start()
    {
        UpdateVelocity();
    }
}
