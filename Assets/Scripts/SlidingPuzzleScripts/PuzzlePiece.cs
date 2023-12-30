using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] int id;

    public int GetId() { return id; }
    public void SetId(int newId) { id = newId; }
}
