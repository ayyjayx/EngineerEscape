using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelState : MonoBehaviour
{
    [SerializeField] GameState[] gameStates;
    [SerializeField] int gamesSolved = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameStates = FindObjectsOfType<GameState>(); 
    }

    // Update is called once per frame
    public void UpdateLevelState(bool isGameSolved)
    {
        if (isGameSolved) { gamesSolved += 1; }
        else { gamesSolved -= 1; }
    }

    private void Update() {
        if (gamesSolved == gameStates.Length)
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("Menu");
        }
    }
}