using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummaryScreen : MonoBehaviour
{
    [SerializeField] TMP_Text timeLeftText;
    [SerializeField] TMP_Text puzzlesSolvedText;
    [SerializeField] TMP_Text resultText;
    [SerializeField] ScoreHandler scoreHandler;
    

    void Start()
    {

        scoreHandler = FindObjectOfType<ScoreHandler>();

        int minuty = Mathf.FloorToInt(scoreHandler.GetTimeLeft() / 60);
        int sekundy = Mathf.FloorToInt(scoreHandler.GetTimeLeft() % 60);

        string formattedText = string.Format("{0:00}:{1:00}", minuty, sekundy);

        if (scoreHandler.GetTimeLeft() <= 0)
        {
            formattedText = "00:00";
        }

        if (scoreHandler.GetTimeLeft() > 0 && scoreHandler.GetGamesSolvedScore() == 3)
        {
            resultText.text = "Sukces!";
            resultText.color = Color.green;
        }
        else
        {
            resultText.text = "Porażka!";
            resultText.color = Color.red;
        }
        timeLeftText.text = "Pozostały czas: " + formattedText;
        puzzlesSolvedText.text = "Rozwiązane zagadki: " + scoreHandler.GetGamesSolvedScore() +"/3";
    }

}
