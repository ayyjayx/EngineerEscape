using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int difficultyLevel = 1;
    public bool expertMode = false;

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