using UnityEngine;
using UnityEngine.UI;

public class Ripple : MonoBehaviour {
    [Header("Animation Settings")]
    public float expandSpeed = 100f;
    public float fadeDuration = 1.0f;
    public AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    private Image rippleImage;
    private RectTransform rectTransform;
    private float startTime;
    private Color originalColor;

    void Awake()
    {
        rippleImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        originalColor = rippleImage.color;
    }

    void OnEnable()
    {
        startTime = Time.unscaledTime;
        rectTransform.sizeDelta = Vector2.zero;
    }

    void Update()
    {
        float elapsed = Time.unscaledTime - startTime;
        float progress = Mathf.Clamp01(elapsed / fadeDuration);

        float scaleProgress = Mathf.SmoothStep(0, 1, progress);
        rectTransform.sizeDelta = Vector2.one * (expandSpeed * scaleProgress * 2);

        float alpha = fadeCurve.Evaluate(progress);
        rippleImage.color = new Color(
            originalColor.r,
            originalColor.g,
            originalColor.b,
            alpha
        );

        if (progress >= 1f) {
            Destroy(gameObject);
        }
    }
}