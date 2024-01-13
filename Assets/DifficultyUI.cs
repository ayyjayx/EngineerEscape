using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DifficultyUI : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] TMP_Text difficultyText;
    [SerializeField] GameObject warning;
    [SerializeField] Toggle expertToggle;

    public void SetDifficultyLevel(int newDifficultyLevel) { gameManager.difficultyLevel = newDifficultyLevel; }

    public void ToggleExpertMode()
    {
        if(expertToggle.isOn)
        {
            gameManager.expertMode = true;
            difficultyText.color = Color.red;
            warning.SetActive(true);
        }
        else
        {
            gameManager.expertMode = false;
            difficultyText.color = Color.black;
            warning.SetActive(false);
        }
    }

    private void Start() {
        gameManager = GameManager.instance;
        if (gameManager.expertMode)
        {
            expertToggle.isOn = true;
        }
        else
        {
            expertToggle.isOn = false;
            difficultyText.color = Color.black;
            warning.SetActive(false);
        }
    }

    void Update()
    {
        int difficultyLevelId = gameManager.difficultyLevel;
        difficultyText.text = difficultyLevelId switch
        {
            1 => "łatwy",
            2 => "średni",
            3 => "trudny",
            _ => "error",
        };
    }
}
