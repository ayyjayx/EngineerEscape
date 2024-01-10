using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockGame : GameState
{
    int DIFFICULTY = 1;
    [SerializeField] RotatingClock[] clocks;
    [SerializeField] MathEquations mathEquations;
    
    [SerializeField] TMP_Text rootClockText;
    [SerializeField] TMP_Text leftClockText;
    [SerializeField] TMP_Text rightClockText;

    void Start()
    {
        clocks = FindObjectsOfType<RotatingClock>();
        RandomizeAndSetOffsets();
    }

    private void RandomizeAndSetOffsets()
    {
        int equationsListCount = mathEquations.GetEquationsTableLenght(DIFFICULTY);

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
    }

    private void Update() {
        if (GetNeedChecking() && GetIsSolved())
        {
            SolveGame();
        }
    }
}
