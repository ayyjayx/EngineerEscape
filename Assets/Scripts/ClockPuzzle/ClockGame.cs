using UnityEngine;

public class ClockGame : GameState
{
    [SerializeField] RotatingClock[] clocks;

    void Start()
    {
        clocks = FindObjectsOfType<RotatingClock>();
    }

    public void CheckIsSolved()
    {
        foreach (RotatingClock clock in clocks)
        {
            /* Jeżeli, którykolwiek zegar nie jest rozwiązany, zagdka nie jest rozwiązana. */
            if (!clock.GetIsClockSolved())
            {
                SetIsSolved(false);
                return;
            }
        }
        SetIsSolved(true);
    }

    private void Update() {
        if (GetNeedChecking() && GetIsSolved())
        {
            levelState.UpdateLevelState(GetIsSolved());
            SetNeedChecking(false);
        }
    }
}
