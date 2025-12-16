using UnityEngine;

public class LaserWarning : MonoBehaviour {
    [Header("Settings")]
    public float expandDuration = 1f;
    public float fadeDuration = 0.05f;
    public Color startColor = Color.yellow;
    public Color endColor = Color.red;
    public float startWidth = 0.05f;
    public float endWidth = 0.2f;

    private LineRenderer lineRenderer;
    private float startTime;
    private bool isExpanding;

    public void Initialize()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void PlayAnimation(Vector2 start, Vector2 end)
    {
        SetPoints(start, end);
        lineRenderer.enabled = true;

        lineRenderer.startColor = startColor;
        lineRenderer.endColor = startColor;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = startWidth;

        startTime = Time.time;
        isExpanding = true;
        enabled = true;
    }

    void Update()
    {
        float elapsed = Time.time - startTime;

        if (isExpanding) {
            float expandProgress = Mathf.Clamp01(elapsed / expandDuration);
            float currentWidth = Mathf.Lerp(startWidth, endWidth, expandProgress);
            lineRenderer.startWidth = currentWidth;
            lineRenderer.endWidth = currentWidth;

            Color currentColor = Color.Lerp(startColor, endColor, expandProgress);
            lineRenderer.startColor = currentColor;
            lineRenderer.endColor = currentColor;

            if (expandProgress >= 1f) {
                isExpanding = false;
                startTime = Time.time;
            }
        } else {
            float fadeProgress = Mathf.Clamp01(elapsed / fadeDuration);
            Color fadedColor = new Color(
                lineRenderer.startColor.r,
                lineRenderer.startColor.g,
                lineRenderer.startColor.b,
                Mathf.Lerp(1f, 0f, fadeProgress)
            );

            lineRenderer.startColor = fadedColor;
            lineRenderer.endColor = fadedColor;

            if (fadeProgress >= 1f) {
                lineRenderer.enabled = false;
                enabled = false;
            }
        }
    }

    public void SetPoints(Vector2 start, Vector2 end)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    public float GetTotalDuration()
    {
        return expandDuration + fadeDuration;
    }
}