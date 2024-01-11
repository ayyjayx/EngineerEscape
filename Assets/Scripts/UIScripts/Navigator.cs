using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour
{
    public void PlayGame() { SceneManager.LoadScene("Level1"); }
    public void ExitGame() { Application.Quit(); }
    public void Settings() { SceneManager.LoadScene("Settings"); }
    public void DifficultySelection() { SceneManager.LoadScene("DifficultySelection"); }
    public void HowToPlay() { SceneManager.LoadScene("HowToPlay"); } 
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
