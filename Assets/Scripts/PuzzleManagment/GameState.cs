using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] protected LevelState levelState;
    [SerializeField] protected bool isSolved = false;
    [SerializeField] protected bool needChecking = true;

    public bool GetIsSolved() { return isSolved; }
    public bool GetNeedChecking() { return needChecking; }

    public void SetIsSolved(bool state) { isSolved = state; }
    public void SetNeedChecking(bool state) { needChecking = state; }

    public void SolveGame()
    {
        levelState.UpdateLevelState(GetIsSolved());
        SetNeedChecking(false);
    }
}
