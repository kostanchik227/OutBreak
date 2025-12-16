using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour {
    public static LevelTimer Instance { get; private set; }
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (GameManager.Instance.GetIsGameStarted() && !GameManager.Instance.GetIsGameEnded()) {
            MathTimeGame();
        }
    }

    private void MathTimeGame()
    {
        elapsedTime += Time.unscaledDeltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}