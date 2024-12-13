using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;

    public AudioClip enemySceneMusic;
    public AudioClip endSceneMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        // Subscribe to scene change event
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (audioSource == null) return;

        switch (scene.name)
        {
            case "enemy scene":
                PlayMusic(enemySceneMusic);
                break;

            case "end scene":
                PlayMusic(endSceneMusic);
                break;

            default:
                audioSource.Stop(); // Stop music if no specific track is set
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // Avoid restarting the same music

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
