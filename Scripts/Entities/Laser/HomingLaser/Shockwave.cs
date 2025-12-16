using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public class Shockwave : MonoBehaviour {
    [Header("Wave Settings")]
    public float duration = 1f;
    public float startRadius = 0.5f;
    public float maxRadius = 4f;        
    public Color startColor = Color.white;

    private SpriteRenderer sr;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        timer = 0f;
        transform.localScale = Vector3.one * startRadius; 
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        float scale = Mathf.Lerp(startRadius, maxRadius, t);
        transform.localScale = Vector3.one * scale;

        Color c = startColor;
        c.a = Mathf.Lerp(1f, 0f, t);
        sr.color = c;

        if (timer >= duration) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            GameManager.Instance.EndGame(false);
        }
    }
}
