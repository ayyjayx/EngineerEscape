using UnityEngine;

public class LevelState : MonoBehaviour
{
    /*
    Skrypt monitorujący obecne postępy w grze.
    */
    [SerializeField] int gamesSolved = 0;

    [SerializeField] GameState[] gameStates;
    [SerializeField] OpenDoor exitDoor;
    [SerializeField] ScoreHandler scoreHandler;

    GameManager gameManager;

    private void Awake() {
        gameManager = GameManager.instance;
    }

    public int GetDifficulty() { return gameManager.difficultyLevel; }

    void Start()
    {
        gameStates = FindObjectsOfType<GameState>();
        exitDoor = FindObjectOfType<OpenDoor>(); // Jednie drzwi wyjściowe powinny posiadać ten skrypt.
    }

    public void UpdateLevelState(bool isGameSolved)
    {
        if (isGameSolved) { gamesSolved += 1; }
        else { gamesSolved -= 1; }
        scoreHandler.SetGamesSolvedScore(gamesSolved);
    }

    private void Update() {
        if (gamesSolved == gameStates.Length)
        {
            exitDoor.SetShouldOpen(true);
        }
    }
}
