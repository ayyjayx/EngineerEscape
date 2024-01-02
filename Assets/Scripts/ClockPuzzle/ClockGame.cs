using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockGame : GameState
{
    [SerializeField] RotatingClock[] clocks;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectsOfType<RotatingClock>();
    }

    public void CheckIsSolved()
    {
        foreach (RotatingClock clock in clocks)
        {
            if (!clock.GetIsClockSolved())
            {
                isSolved = false;
                return;
            }
        }
        isSolved = true;
    }
    private void Update() {
        if (needChecking && GetIsSolved())
        {
            levelState.UpdateLevelState(GetIsSolved());
            needChecking = false;
        }
    }
}
