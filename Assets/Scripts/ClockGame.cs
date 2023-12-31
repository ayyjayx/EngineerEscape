using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockGame : MonoBehaviour
{
    [SerializeField] RotatingClock[] clocks;
    [SerializeField] bool isSolved;

    public bool GetIsSolved() { return isSolved; }
    // Start is called before the first frame update
    void Start()
    {
        FindObjectsOfType<RotatingClock>();
    }

    public void CheckIsSolved()
    {
        foreach (RotatingClock clock in clocks)
        {
            if (!clock.GetIsSolved())
            {
                isSolved = false;
                return;
            }
        }
        isSolved = true;
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
