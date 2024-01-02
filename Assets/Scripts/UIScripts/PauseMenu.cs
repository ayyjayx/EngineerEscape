using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        if (pauseMenu != null) { pauseMenu.SetActive(false); }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (isPaused) { ResumeGame(); }
            else { PauseGame(); }
        }

    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
