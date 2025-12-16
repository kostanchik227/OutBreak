using UnityEngine;

public class StartButton : MonoBehaviour
{
    public static StartButton Instance;

    public Camera mainCamera;
    public Canvas canvas;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetPosition();
    }

    public void StartGameButtonClicked()
    {
        if (!GameManager.Instance.GetIsGameStarted()) {
            GameManager.Instance.StartGame();
        } else {
            GameManager.Instance.GamePause(false);
        }
        PulsingWaveButton.Instance.ClearRipples();
    }

    public void SetPosition()
    {
        Vector2 screenPos = mainCamera.WorldToScreenPoint(FollowMouse.Instance.GetPositionPlatform());
        gameObject.transform.position = screenPos;
    }
}
