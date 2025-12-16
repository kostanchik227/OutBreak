using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PulsingWaveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public static PulsingWaveButton Instance;

    [Header("Main Settings")]
    public GameObject ripplePrefab;

    [Header("Pulse Settings")]
    public float basePulseSpeed = 2f;
    public float basePulseStrength = 0.05f;
    public float hoverPulseStrength = 0.1f;

    [Header("Wave Settings")]
    public float waveSpawnInterval = 1.5f;
    public float hoverWaveSpawnInterval = 0.5f;

    private RectTransform rectTransform;
    private bool isHovered = false;
    private float waveTimer = 0f;

    void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        waveTimer = 100f;
    }

    void Update()
    {
        float strength = isHovered ? hoverPulseStrength : basePulseStrength;
        float scale = 1f + Mathf.Sin(Time.unscaledTime * basePulseSpeed) * strength;
        rectTransform.localScale = new Vector3(scale, scale, 1f);

        waveTimer += Time.unscaledDeltaTime;
        float interval = isHovered ? hoverWaveSpawnInterval : waveSpawnInterval;

        if (ripplePrefab != null && waveTimer >= interval) {
            SpawnRipple();
            waveTimer = 0f;
        }
    }

    private void SpawnRipple()
    {
        GameObject ripple = Instantiate(ripplePrefab, transform.parent);
        ripple.transform.SetAsFirstSibling();
        ripple.transform.position = transform.position;
    }

    public void ClearRipples()
    {
        waveTimer = 100f;
        Transform parent = transform.parent;

        for (int i = parent.childCount - 1; i >= 0; i--) {
            Transform child = parent.GetChild(i);

            if (child.GetComponent<Ripple>() != null) {
                Destroy(child.gameObject);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
