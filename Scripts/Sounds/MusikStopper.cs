using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicStopper : MonoBehaviour {
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SoundManager.Instance.StopMusic();
    }
}
