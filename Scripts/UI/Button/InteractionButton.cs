using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManualButtonController : MonoBehaviour {
    [Header("Visual Settings")]
    public Sprite normalSprite;
    public Sprite pressedSprite;
    public Color normalColor = Color.white;
    public Color pressedColor = Color.gray;
    public float colorTransitionDuration = 0.2f;

    [Header("Scale Settings")]
    public Vector3 pressedScale = new Vector3(0.9f, 0.9f, 1f);
    public float scaleTransitionDuration = 0.1f;

    private Image buttonImage;
    private Vector3 originalScale;
    private bool isPressed = false;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        originalScale = transform.localScale;

        SetToNormalState();
    }

    public void SetToPressedState()
    {
        if (isPressed) return;
        isPressed = true;

        if (pressedSprite != null) {
            buttonImage.sprite = pressedSprite;
        }

        if (colorTransitionDuration > 0) {
            StartCoroutine(TransitionColor(pressedColor));
        } else {
            buttonImage.color = pressedColor;
        }

        if (scaleTransitionDuration > 0) {
            StartCoroutine(TransitionScale(pressedScale));
        } else {
            transform.localScale = pressedScale;
        }
    }

    public void SetToNormalState()
    {
        if (!isPressed) return;
        isPressed = false;

        if (normalSprite != null) {
            buttonImage.sprite = normalSprite;
        }

        if (colorTransitionDuration > 0) {
            StartCoroutine(TransitionColor(normalColor));
        } else {
            buttonImage.color = normalColor;
        }

        if (scaleTransitionDuration > 0) {
            StartCoroutine(TransitionScale(originalScale));
        } else {
            transform.localScale = originalScale;
        }
    }

    public void ToggleButtonState()
    {
        if (isPressed) {
            SetToNormalState();
        } else {
            SetToPressedState();
        }
    }

    public void PressButtonTemporarily(float pressDuration)
    {
        if (isPressed) return;

        StartCoroutine(TemporaryPressRoutine(pressDuration));
    }

    private IEnumerator TemporaryPressRoutine(float duration)
    {
        SetToPressedState();
        yield return new WaitForSeconds(duration);
        SetToNormalState();
    }

    private IEnumerator TransitionColor(Color targetColor)
    {
        Color startColor = buttonImage.color;
        float elapsed = 0f;

        while (elapsed < colorTransitionDuration) {
            Debug.Log(Time.timeScale);
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / colorTransitionDuration);
            buttonImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        buttonImage.color = targetColor;
    }

    private IEnumerator TransitionScale(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < scaleTransitionDuration) {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / scaleTransitionDuration);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        transform.localScale = targetScale;
    }
}