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
        int leftEquationsListCount = mathEquations.GetEquationsListCount("left", DIFFICULTY);
        int rightEquationsListCount = mathEquations.GetEquationsListCount("right", DIFFICULTY);

        List<MathEquations.EquationData> leftEquations = mathEquations.equationsSides.GetListForChosenSide("left", DIFFICULTY);
        List<MathEquations.EquationData> rightEquations = mathEquations.equationsSides.GetListForChosenSide("right", DIFFICULTY);

        foreach (RotatingClock clock in clocks)
        {
            if (!clock.IsRootClock())
            {
                int leftRandomIndex = Random.Range(0, leftEquationsListCount);
                int rightRandomIndex = Random.Range(0, rightEquationsListCount);

                switch (clock.GetSide())
                {
                    case "left":
                        clock.SetOffset(leftEquations[leftRandomIndex].solution);
                        clock.SetCorrespondingMapText(leftEquations[leftRandomIndex].equation);
                        leftEquations.RemoveAt(leftRandomIndex);
                        leftEquationsListCount -= 1;
                        break;
                    case "right":
                        clock.SetOffset(rightEquations[rightRandomIndex].solution);
                        clock.SetCorrespondingMapText(rightEquations[rightRandomIndex].equation);
                        rightEquations.RemoveAt(rightRandomIndex);
                        rightEquationsListCount -= 1;
                        break;
                    default:
                        break;
                }
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
            levelState.audioManager.PlaySFX(levelState.audioManager.success);
            SolveGame();
        }
    }
}
