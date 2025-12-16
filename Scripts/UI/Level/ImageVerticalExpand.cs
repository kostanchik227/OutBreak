using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageVerticalExpand : MonoBehaviour {              
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void ExpandImage(float targetHeight, float duration)
    {
        StartCoroutine(ExpandHeight(targetHeight, duration));
    }

    IEnumerator ExpandHeight(float targetHeight, float duration)
    {
        float startHeight = rectTransform.sizeDelta.y;
        float time = 0f;

        while (time < duration) {
            time += Time.unscaledDeltaTime;
            float t = time / duration;
            t = t * t * (3f - 2f * t);

            float newHeight = Mathf.Lerp(startHeight, targetHeight, t);

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);

            yield return null;
        }

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, targetHeight);
    }
}