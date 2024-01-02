using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] protected LevelState levelState;
    [SerializeField] protected bool isSolved = false;
    [SerializeField] protected bool needChecking = true;

    public bool GetIsSolved() { return isSolved; }
}
