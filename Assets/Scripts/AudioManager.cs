using System.Collections;
using System.Collections.Generic;
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
    public AudioClip test_sfx;

    // public bool is_playing = false;
    // public static AudioManager instance;
    // private void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }

    //     if (SceneManager.GetActiveScene().buildIndex == 1)
    //     {
    //         PlayGameMusic();
    //     }
    //     else
    //     {
    //         PlayMenuMusic();
    //     }
    //     Debug.Log(is_playing);
    // }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            musicSource.Stop();
            musicSource.clip = game;
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
            musicSource.clip = menu;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // public void PlayGameMusic()
    // {
    //     musicSource.Stop();
    //     musicSource.clip = game;
    //     musicSource.Play();
    //     is_playing = false;
    // }

    // public void PlayMenuMusic()
    // {
    //     if (is_playing == false)
    //     {
    //         musicSource.Stop();
    //         musicSource.clip = menu;
    //         musicSource.Play();
    //         is_playing = true;
    //     }

    // }
}
