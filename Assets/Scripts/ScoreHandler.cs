using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreHandler : MonoBehaviour
{
    /* Klasa przechowujące dane o wynikach rozgrywki. */
    public static ScoreHandler instance;

    private int gamesSolvedScore;
    private int gamesNumber;
    [SerializeField] private float timeLeft = 60f;

    public void SetTimeLeft(float time) { timeLeft = time; }
    public void SetGamesNumber(int number) { gamesNumber = number; }
    public void SetGamesSolvedScore(int score) { gamesSolvedScore = score; }

    public float GetTimeLeft() { return timeLeft; }
    public int GetGamesNumber() { return gamesNumber; }
    public int GetGamesSolvedScore() { return gamesSolvedScore; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
