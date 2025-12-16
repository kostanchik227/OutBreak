using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private bool _isGameRunning = false;
    private bool isGameStarted = false;
    private bool isGameEnded = false;
    private int numberBricks;
    private AttemptData resultData;

    public GameObject Ball;
    public GameObject bricks;
    public GameObject winUI;
    public GameObject pauseUI;
    public GameObject loseUI;
    public GameObject startUI;
    public int numberScene;
    private bool isGameRunning
    {
        get => _isGameRunning;
        set
        {
            _isGameRunning = value;
            if (_isGameRunning) {
                Time.timeScale = 1;
            } else {
                Time.timeScale = 0;
            }
        }
    }

    private static bool s_cameFromReload = false;
    private void Awake()
    {
        isGameRunning = false;
        numberScene = SceneManager.GetActiveScene().buildIndex;
        Application.targetFrameRate = 60;
        Instance = this;
        numberBricks = bricks.transform.childCount;

        if (s_cameFromReload) {
            s_cameFromReload = false;
            startUI.SetActive(true);
        } else {
            if (PlayerPrefs.GetInt("CameFromScene", 0) == numberScene) {
                startUI.SetActive(false);
                PlayerPrefs.SetInt("CameFromScene", 0);
            }
        }
    }

    public int GetNumberScene()
    {
        return numberScene;
    }
    public bool GetIsGameStarted()
    {
        return isGameStarted;
    }
    public bool GetIsGameEnded()
    {
        return isGameEnded;
    }
    public bool GetIsGameRunning()
    {
        return isGameRunning;
    }

    public AttemptData GetResultGame()
    {
        if (!isGameEnded)
            return null;
        return resultData;
    }

    public void SendResultGame(bool resultGame)
    {
        resultData = new AttemptData();
        resultData.passed = resultGame;
        resultData.time = LevelTimer.Instance.GetElapsedTime();
        resultData.bricksDestroyed = numberBricks - bricks.transform.childCount;
        resultData.bricksNotDestroyed = bricks.transform.childCount;
        SaveLevellPlayerData.Instance.SaveData(resultData);
    }

    public void StartGame()
    {
        startUI.SetActive(false);
        isGameStarted = true;
        isGameRunning = true;
        BallMovement ball = Ball.GetComponent<BallMovement>();
        ball.Launch();
        LaserManager.Instance.RunScript();
    }

    public void EndGame(bool resultGame)
    {
        SendResultGame(resultGame);

        isGameRunning = false;
        isGameEnded = true;
        if (resultGame) {
            winUI.SetActive(true);
            SoundManager.Instance.Play("WinSound");
        } else {
            loseUI.SetActive(true);
            SoundManager.Instance.Play("LoseSound");
        }
    }

    public void CheckDestroyAllBricks(int countBricks = 0)
    {
        if (bricks.transform.childCount == countBricks) {
            EndGame(true);
        }
    }

    public void GamePause(bool setGamePause)
    {
        if (!isGameStarted || isGameEnded) {
            return;
        }
        if (isGameRunning && setGamePause) {
            pauseUI.SetActive(true);
            StartButton.Instance.SetPosition();
            isGameRunning = false;
        } else if (!isGameRunning && !setGamePause) {
            pauseUI.SetActive(false);
            isGameRunning = true;
        }
    }

    public void ReloadLevel()
    {
        s_cameFromReload = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Update()
    {
        if (!isGameEnded) {
            GameManager.Instance.CheckDestroyAllBricks(0);
        }

        if (GameInput.Instance.GetIsClickR()) {
            ReloadLevel();
        }
        if (GameInput.Instance.GetIsClickSpace()) {
            GamePause(true);
        }
        if (GameInput.Instance.GetIsClickEsc()) {
            GoMenu();
        }
    }
}
