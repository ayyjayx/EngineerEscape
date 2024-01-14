using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip menu;
    public AudioClip game;
    public AudioClip button;
    public AudioClip rotateClock;
    public AudioClip success;

    [SerializeField] private AudioMixer myMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume"))
        {
            float musicValue = PlayerPrefs.GetFloat("musicVolume");
            myMixer.SetFloat("music", Mathf.Log10(musicValue) * 20);
            float SFXValue = PlayerPrefs.GetFloat("sfxVolume");
            myMixer.SetFloat("sfx", Mathf.Log10(SFXValue) * 20);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level1" && musicSource.clip != game)
        {
            musicSource.Stop();
            musicSource.clip = game;
            musicSource.Play();
        }
        else if (SceneManager.GetActiveScene().name != "Level1" && musicSource.clip != menu)
        {
            musicSource.Stop();
            musicSource.clip = menu;
            musicSource.Play();
        }
    }

    // private void Start()
    // {
    //     if (SceneManager.GetActiveScene().buildIndex == 1)
    //     {
    //         musicSource.Stop();
    //         musicSource.clip = game;
    //         musicSource.Play();
    //     }
    //     else
    //     {
    //         musicSource.Stop();
    //         musicSource.clip = menu;
    //         musicSource.Play();
    //     }
    // }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void UnpauseMusic()
    {
        musicSource.UnPause();
    }

    // public void PlayGameMusic()
    // {
    //     musicSource.Stop();
    //     musicSource.clip = game;
    //     musicSource.Play();
    // }

    // public void PlayMenuMusic()
    // {
    //     musicSource.Stop();
    //     musicSource.clip = menu;
    //     musicSource.Play();
    // }
}
