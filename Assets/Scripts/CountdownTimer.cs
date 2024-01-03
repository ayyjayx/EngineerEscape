using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    [SerializeField] ScoreHandler scoreHandler;
    TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TMP_Text>();
        timerText.text = "START";
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreHandler.timeLeft > 0) {
            scoreHandler.timeLeft -= Time.deltaTime; 

            int minuty = Mathf.FloorToInt(scoreHandler.timeLeft / 60);
            int sekundy = Mathf.FloorToInt(scoreHandler.timeLeft % 60);

            string formattedText = string.Format("{0:00}:{1:00}", minuty, sekundy);

            timerText.text = "Pozostało: " + formattedText;
        }
        else
        {
            timerText.text = "Czas minął!";
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("Summary");
        }

    }
}