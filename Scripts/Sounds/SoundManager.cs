using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using static SoundManager;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    [System.Serializable]
    public class Sound {
        public string name;
        public string typeSound;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [SerializeField] private Sound[] sounds;

    private Dictionary<string, Sound> soundDict;
    private AudioSource musicSource;
    private AudioSource effectsSource;


    [Header("Menu Playlist")]
    [SerializeField] private List<AudioClip> menuMusicClips = new List<AudioClip>();
    private int currentTrackIndex = 0;
    private Coroutine playlistCoroutine;
    private bool applicationPaused = false;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        musicSource = gameObject.AddComponent<AudioSource>();
        effectsSource = gameObject.AddComponent<AudioSource>();

        soundDict = new Dictionary<string, Sound>();

        foreach (var s in sounds) {
            if (!soundDict.ContainsKey(s.name))
                soundDict.Add(s.name, s);
        }

        Application.runInBackground = true;
        musicSource.ignoreListenerPause = true;
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
        if (scene.buildIndex == 0) {
            if (playlistCoroutine == null)
                StartMenuPlaylist();
        } else {
            StopMusic();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        applicationPaused = !hasFocus;
        if (hasFocus && SceneManager.GetActiveScene().buildIndex == 0) {
            if (playlistCoroutine == null)
                StartMenuPlaylist();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        applicationPaused = pauseStatus;
    }

    public void Play(string name)
    {
        if (soundDict.TryGetValue(name, out var sound)) {
            SettingsData settingsData = SettingsLoader.Instance.GetSettingsData();
            float volume = sound.volume * settingsData.globalVolume;

            if (sound.typeSound == "effects") {
                volume *= settingsData.effectsVolume;

                effectsSource.PlayOneShot(sound.clip, volume);  
            } else if (sound.typeSound == "musik") {
                musicSource.ignoreListenerPause = true;
                volume *= settingsData.musikVolume;
                musicSource.clip = sound.clip;
                musicSource.volume = volume;
                musicSource.loop = true;
                musicSource.Play();
            }
        } else {
            Debug.LogWarning($"Sound '{name}' not found in SoundManager.");
        }
    }

    public void StopMusic()
    {
        if (playlistCoroutine != null) {
            StopCoroutine(playlistCoroutine);
            playlistCoroutine = null;
        }
        if (musicSource.isPlaying)
            musicSource.Stop();
    }

    public void SetGlobalVolume(float volume)
    {
        SettingsData data = SettingsLoader.Instance.GetSettingsData();
        data.globalVolume = Mathf.Clamp01(volume);
        SettingsLoader.Instance.SaveData(data);
    }

    public void SetEffectsVolume(float volume)
    {
        SettingsData data = SettingsLoader.Instance.GetSettingsData();
        data.effectsVolume = Mathf.Clamp01(volume);
        SettingsLoader.Instance.SaveData(data);
    }

    public void SetMusikVolume(float volume)
    {
        SettingsData data = SettingsLoader.Instance.GetSettingsData();
        data.musikVolume = Mathf.Clamp01(volume);
        SettingsLoader.Instance.SaveData(data);
    }

    public void StartMenuPlaylist()
    {
        if (menuMusicClips.Count == 0) {
            Debug.LogWarning("Menu playlist is empty!");
            return;
        }

        StopMusic();
        currentTrackIndex = 0;
        playlistCoroutine = StartCoroutine(MenuPlaylistRoutine());
    }

    private IEnumerator MenuPlaylistRoutine()
    {
        while (true) {
            if (applicationPaused) {
                yield return null;
                continue;
            }
            AudioClip clip = menuMusicClips[currentTrackIndex];

            SettingsData settingsData = SettingsLoader.Instance.GetSettingsData();
            musicSource.clip = clip;
            musicSource.volume = settingsData.globalVolume * settingsData.musikVolume;
            musicSource.loop = false; 
            musicSource.Play();

            yield return new WaitWhile(() => musicSource.isPlaying);

            currentTrackIndex++;
            if (currentTrackIndex >= menuMusicClips.Count)
                currentTrackIndex = 0;
        }
    }

    private void Update()
    {
        if (musicSource != null && SettingsLoader.Instance != null) {
            SettingsData settings = SettingsLoader.Instance.GetSettingsData();
            musicSource.volume = settings.globalVolume * settings.musikVolume;
        }
    }
}
