using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public int numberLevel = 1;
    public float duration = 0.1f;

    private Button button;
    private Vector3 originalScale;
    private Coroutine scalingCoroutine;
    private Coroutine fadeRoutine;
    private CanvasGroup infoPanel;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        originalScale = transform.localScale;
        infoPanel = MenuDisplayLevelData.Instance.gameObject.GetComponent<CanvasGroup>();
    }

    void OnButtonClicked()
    {
        MainMenu.Instance.PlayGame(numberLevel);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //infoPanel.alpha = 0f;
        MenuDisplayLevelData.Instance.Display(numberLevel);
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(DisplayAlfa(duration, 1f, infoPanel));

        if (scalingCoroutine != null) StopCoroutine(scalingCoroutine);
        scalingCoroutine = StartCoroutine(ScaleTo(new Vector3(1.05f, 1.1f, 0f), duration));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        //infoPanel.alpha = 1f;
        fadeRoutine = StartCoroutine(DisplayAlfa(duration, 0f, infoPanel));
        
        if (scalingCoroutine != null) StopCoroutine(scalingCoroutine);
        scalingCoroutine = StartCoroutine(ScaleTo(originalScale, duration));
    }

    private IEnumerator ScaleTo(Vector3 target, float time)
    {
        Vector3 start = transform.localScale;
        float elapsed = 0f;

        while (elapsed < time) {
            transform.localScale = Vector3.Lerp(start, target, elapsed / time);
            elapsed += Time.unscaledDeltaTime; 
            yield return null;
        }

        transform.localScale = target; 
    }

    private IEnumerator DisplayAlfa(float time, float to, CanvasGroup cg)
    {
        float elapsed = 0f;
        float start = cg.alpha;

        while (elapsed < time) {
            cg.alpha = Mathf.Lerp(start, to, elapsed / time);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        cg.alpha = to;
    }
}
