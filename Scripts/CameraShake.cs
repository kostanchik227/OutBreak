using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
    public static CameraShake Instance;

    private Vector3 initialPosition;
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        Instance = this;
        initialPosition = transform.localPosition;
    }

    public void ShakeForDuration(float duration, float strength)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeCoroutine(duration, strength));
    }

    private IEnumerator ShakeCoroutine(float duration, float strength)
    {
        float elapsed = 0f;

        while (elapsed < duration) {
            float t = elapsed / duration;

            float currentStrength = strength * Mathf.Sin(t * Mathf.PI);

            Vector3 offset = Random.insideUnitCircle * currentStrength;
            transform.localPosition = initialPosition + offset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialPosition;
        shakeCoroutine = null;
    }
}