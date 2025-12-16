using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
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
        LaserManager.Instance.RunScript();
    }
    public void PlayGame(int numberLevel)
    {
        if (numberLevel > 0 && numberLevel < 100) {
            SceneManager.LoadScene(numberLevel);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
