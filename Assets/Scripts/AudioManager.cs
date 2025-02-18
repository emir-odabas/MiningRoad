using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("----Audio Source----")]
    [SerializeField] private AudioSource musicSource;

    [Header("----Audio Clips----")]
    [SerializeField] private AudioClip mainMenuMusic; // Scene 0 için
    [SerializeField] private AudioClip inGameMusic;   // Scene 1 için

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Sahne geçişlerinde AudioManager yok edilmesin
    }

    private void Start()
    {
        // Sahne yüklendiğinde uygun müziği çal
        ChangeMusicForScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.sceneLoaded += OnSceneLoaded; // Yeni sahne yüklendiğinde çağrılacak
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeMusicForScene(scene.buildIndex); // Sahne değiştiğinde müziği değiştir
    }

    private void ChangeMusicForScene(int sceneIndex)
    {
        AudioClip newClip = null;

        if (sceneIndex == 0) // Scene 0 için müzik
        {
            newClip = mainMenuMusic;
        }
        else if (sceneIndex == 1) // Scene 1 için müzik
        {
            newClip = inGameMusic;
        }

        if (newClip != null && musicSource.clip != newClip)
        {
            musicSource.Stop();
            musicSource.clip = newClip;
            musicSource.Play();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Dinleyiciyi temizle
    }

}
