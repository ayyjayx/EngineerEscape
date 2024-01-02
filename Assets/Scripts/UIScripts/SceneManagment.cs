using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public void PlayGame() { SceneManager.LoadScene("Level1"); }
    public void ExitGame() { Application.Quit(); }
    public void Settings() { SceneManager.LoadScene("Settings"); }
    public void HowToPlay() { SceneManager.LoadScene("HowToPlay"); } 
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
