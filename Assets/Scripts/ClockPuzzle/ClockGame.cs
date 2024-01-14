using System.Collections.Generic;
using UnityEngine;

public class ClockGame : GameState
{
    int DIFFICULTY;
    
    [SerializeField] RotatingClock[] clocks;
    [SerializeField] MathEquations mathEquations;
    [SerializeField] Material solvedMaterial;

    void Start()
    {
        clocks = FindObjectsOfType<RotatingClock>();
        DIFFICULTY = levelState.GetDifficulty();
        RandomizeAndSetOffsets();
    }

    private void RandomizeAndSetOffsets()
    {
        int equationsListCount = mathEquations.GetEquationsListCount(DIFFICULTY);

        List<MathEquations.EquationData> equations = mathEquations.equationsList.GetList(DIFFICULTY);

        foreach (RotatingClock clock in clocks)
        {
            if (!clock.IsRootClock())
            {
                int randomIndex = Random.Range(0, equationsListCount);
                clock.SetOffset(equations[randomIndex].solution);
                clock.SetCorrespondingMapText(equations[randomIndex].equation);
                equations.RemoveAt(randomIndex);
                equationsListCount -= 1;
                clock.UpdateClockState(clock.GetCurrentHourWithOffset());
            }
        }
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

        foreach (RotatingClock clock in clocks)
        {
            clock.GetComponent<MeshRenderer>().material = solvedMaterial;
        }
    }

    private void Update() {
        if (GetNeedChecking() && GetIsSolved())
        {
            SolveGame();
        }
    }
}
