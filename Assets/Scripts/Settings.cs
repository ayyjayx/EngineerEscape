using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider sensitivitySlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume")) { LoadVolume(); }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }

        if (!PlayerPrefs.HasKey("sensitivity")) { SetSensitivity(); }
        else { sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity"); }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetSensitivity()
    {
        float sens = sensitivitySlider.value;
        PlayerPrefs.SetFloat("sensitivity", sens);
    }
}