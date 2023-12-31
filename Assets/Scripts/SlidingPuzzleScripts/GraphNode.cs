using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GraphNode : MonoBehaviour
{
    /*
    Klasa reprezentująca wezeł grafu.
     */
    
    [SerializeField] int id;                                                 // Id węzła.
    [SerializeField] PuzzlePiece currentPuzzle = null;                       // Puzel obecnie znajdujący się na węźle.
    [SerializeField] List<GraphNode> neighbours = new();                     // Lista połączonych węzłów.
    [SerializeField] SlidingPuzzle slidingPuzzle;

    public void SetId(int newId) { id = newId; }
    public int GetId() { return id; }                                                   
    public PuzzlePiece GetCurrentPuzzle() { return currentPuzzle; }          
    public void SetPuzzlePiece(PuzzlePiece newPuzzle, bool isMoving) {
        currentPuzzle = newPuzzle;
        if (!isMoving) {newPuzzle.transform.position = transform.position; }
    }
    public PuzzlePiece RemovePuzzle() {
        PuzzlePiece removedPuzzle = GetCurrentPuzzle();
        currentPuzzle = null;
        return removedPuzzle;
    }
    public List<GraphNode> GetNeighboursList() { return neighbours; }

    public bool IsMatched()
    {
        if (!(currentPuzzle == null))
        {
            if (id == currentPuzzle.GetId()) { return true; }
        }
        return false;
        
    }

    public bool IsEmpty()
    {
        if (currentPuzzle == null) { return true; }
        else return false;
    }

    private void OnMouseUpAsButton() {
        Debug.Log("Click detected.");
        if (!slidingPuzzle.GetIsMoving() && !slidingPuzzle.GetIsSolved()) { slidingPuzzle.MakeMove(this); }
    }

    public GraphNode FindEmptyNeighbour()
    {
        foreach (GraphNode neighbour in neighbours)
        {
            if (neighbour.IsEmpty()) { return neighbour; }
        } 
        return null;
    }

    private void Start() {
        slidingPuzzle = FindObjectOfType<SlidingPuzzle>();
    }
}
