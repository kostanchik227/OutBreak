using UnityEngine;

public class LaserHoming : MonoBehaviour {
    public Transform target;      
    public float moveSpeed = 5f;   
    public float turnSpeed = 200f;
    public float lifetime = 10f;

    public float blinkTime = 2f;               
    public float blinkStartInterval = 0.4f;     
    public float blinkEndInterval = 0.05f;      

    public GameObject explosionPrefab;

    [Header("Цвета")] 
    public Color blinkColor = Color.white;
    public Color startColor = Color.white;    
    public Color endColor = Color.clear;

    private bool exploding = false;
    private SpriteRenderer sr;
    private float liveTimer = 0f;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = startColor;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 toTarget = (Vector2)target.position - (Vector2)transform.position;
        toTarget.Normalize();

        Vector2 forward = transform.right;

        float angle = Vector3.SignedAngle(forward, toTarget, Vector3.forward);
        float step = turnSpeed * Time.deltaTime;
        float clamped = Mathf.Clamp(angle, -step, step);

        transform.Rotate(0, 0, clamped);
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime, Space.Self);


        liveTimer += Time.deltaTime;
        float t = Mathf.Clamp01(liveTimer / lifetime);

        float acceleratedT = t * t * t;

        Color c = Color.Lerp(startColor, endColor, acceleratedT);
        c.a = startColor.a;
        sr.color = c;

        if (t >= 1f) {
            Explode();
        }
    }

    private void Start()
    {
        //Explode();
    }

    public void Explode()
    {
        if (!exploding)
            StartCoroutine(BlinkAndExplode());
    }

    private System.Collections.IEnumerator BlinkAndExplode()
    {
        exploding = true;
        float timer = 0f;
        bool toggle = false;

        while (timer < blinkTime) {
            toggle = !toggle;
            if (sr != null)
                sr.color = toggle ? blinkColor : startColor;

            float t = timer / blinkTime;
            float currentInterval = Mathf.Lerp(blinkStartInterval, blinkEndInterval, t);

            yield return new WaitForSeconds(currentInterval);
            timer += currentInterval;
        }

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            blinkTime = 0;
            Explode();
            GameManager.Instance.EndGame(false);
        }
    }
}