using UnityEngine;
using System.Collections;

public class ControllerCanvas : MonoBehaviour
{
    public ImageVerticalExpand verticalExpand;
    public float timeWait = 1.0f;
    public float durationExpand = 1.0f;
    public float targetHeight = 500f;
    public GameObject dataEnable;
    public GameObject textAnimation;
    public GameObject topLineAnimation;
    public GameObject bottomLineAnimation;
    public float fadeDuration = 0.01f;

    private RectTransform rectTransformTextAnimation;
    private RectTransform rectTransformTopLineAnimation;
    private RectTransform rectTransformBottomLineAnimation;
    void Start()
    {
        rectTransformTextAnimation = textAnimation.GetComponent<RectTransform>();
        rectTransformTopLineAnimation = topLineAnimation.GetComponent<RectTransform>();
        rectTransformBottomLineAnimation = bottomLineAnimation.GetComponent<RectTransform>();
        if (verticalExpand != null) {
            StartCoroutine(StartWithDelay());
        } else {
            Debug.Log("ControllerCanvas -> verticalExpand is null");
        }
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSecondsRealtime(timeWait);
        verticalExpand.ExpandImage(targetHeight, durationExpand);
        RaiseObjectLinear(targetHeight/2, durationExpand, rectTransformTextAnimation);
        topLineAnimation.SetActive(true);
        bottomLineAnimation.SetActive(true);
        RaiseObjectLinear(targetHeight/2-1, durationExpand, rectTransformTopLineAnimation);
        RaiseObjectLinear(-targetHeight/2+1, durationExpand, rectTransformBottomLineAnimation);

        if (durationExpand > 0)
            yield return new WaitForSecondsRealtime(durationExpand);
        if (dataEnable != null) {
            dataEnable.GetComponent<EndGameDisplayLevelData>().DisplayEndGameMenu(GameManager.Instance.GetResultGame(), 
                                                                        GameManager.Instance.GetNumberScene());
            dataEnable.SetActive(true);

            CanvasGroup cg = dataEnable.GetComponent<CanvasGroup>();
            if (cg == null)
                cg = dataEnable.AddComponent<CanvasGroup>();

            cg.alpha = 0f;

            float time = 0f;
            while (time < fadeDuration) {
                time += Time.unscaledDeltaTime;
                cg.alpha = Mathf.SmoothStep(0f, 1f, time / fadeDuration);
                yield return null;
            }
            cg.alpha = 1f;
        }
    }

    public void RaiseObjectLinear(float offsetY, float duration, RectTransform rectTransformAnimation)
    {
        StartCoroutine(RaiseObjectLinearCor(offsetY, duration, rectTransformAnimation));
    }

    IEnumerator RaiseObjectLinearCor(float offsetY, float duration, RectTransform rectTransformAnimation)
    {
        float startY = rectTransformAnimation.anchoredPosition.y;
        float endY = startY + offsetY;
        float time = 0f;

        while (time < duration) {
            time += Time.unscaledDeltaTime;
            float t = time / duration;
            t = t * t * (3f - 2f * t);

            float newY = Mathf.Lerp(startY, endY, t);

            rectTransformAnimation.anchoredPosition = new Vector2(
                rectTransformAnimation.anchoredPosition.x,
                newY
            );

            yield return null;
        }

        rectTransformAnimation.anchoredPosition = new Vector2(
            rectTransformAnimation.anchoredPosition.x,
            endY
        );
    }


}
