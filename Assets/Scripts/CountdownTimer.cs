using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    [SerializeField] ScoreHandler scoreHandler;
    TMP_Text timerText;

    void Start()
    {
        timerText = GetComponent<TMP_Text>();
        timerText.text = "START";
    }

    void Update()
    {
        if (scoreHandler.GetTimeLeft() > 0) {
            scoreHandler.SetTimeLeft(scoreHandler.GetTimeLeft() - Time.deltaTime);

            int minuty = Mathf.FloorToInt(scoreHandler.GetTimeLeft() / 60);
            int sekundy = Mathf.FloorToInt(scoreHandler.GetTimeLeft() % 60);

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