using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GraphNode : MonoBehaviour
{
    /*
    Klasa reprezentująca wezeł grafu.
     */
    
    [SerializeField] int id;
    [SerializeField] PuzzlePiece currentPuzzle = null;
    [SerializeField] List<GraphNode> neighbours = new();
    [SerializeField] SlidingPuzzle slidingPuzzle;
    [SerializeField] int interactableDistance = 8;

    AudioManager audioManager;

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
        audioManager.PlaySFX(audioManager.button);
        if (!slidingPuzzle.GetIsMoving() && !slidingPuzzle.GetIsSolved() && IsButtonInRange()) { slidingPuzzle.MakeMove(this); }
    }

    private bool IsButtonInRange()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Transform buttonPosition = GetComponent<Transform>();
        float distance = Vector3.Distance(player.transform.position, buttonPosition.position);
        if (distance > interactableDistance)
        {
            return false;
        }
        else return true;
    }

    public GraphNode FindEmptyNeighbour()
    {
        foreach (GraphNode neighbour in neighbours)
        {
            if (neighbour.IsEmpty()) { return neighbour; }
        } 
        return null;
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start() {
        interactableDistance = 8; // coś sie zepsuło więc na razie na sztywno to ustawiam jeszcze raz :ddddd
        slidingPuzzle = FindObjectOfType<SlidingPuzzle>();
    }
}
