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



    // public void PlayMenuMusic()
    // {
    //     musicSource.clip = menu;
    //     musicSource.Play();
    // }

    // public void PlayGameMusic()
    // {
    //     musicSource.clip = game;
    //     musicSource.Play();
    // }
}
