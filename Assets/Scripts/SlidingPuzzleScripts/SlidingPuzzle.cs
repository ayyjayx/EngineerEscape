using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlidingPuzzle : GameState
{

    int DIFFICULTY = 1;

    [SerializeField] GraphNode[] nodes;
    [SerializeField] List<GraphNode> matchedPuzzles;
    [SerializeField] GameObject puzzleObject;
    [SerializeField] List<Material> puzzleMaterials;
    [SerializeField] Material solvedMaterial;

    [SerializeField] GameObject easyPreset;
    [SerializeField] GameObject mediumPreset;
    [SerializeField] GameObject hardPreset;

    private bool isMoving = false;

    public bool GetIsMoving() { return isMoving; }
    public void SetIsMoving(bool state) { isMoving = state; }

    void Start()
    {
        DIFFICULTY = levelState.GetDifficulty();
        InstantiatePuzzlePresset();
        nodes = FindObjectsOfType<GraphNode>();
        InitializeNodes();
        InitializePuzzles();
    }

    private void InstantiatePuzzlePresset()
    {
        if (DIFFICULTY == 1) { Instantiate(easyPreset, transform); }
        else if (DIFFICULTY == 2) { Instantiate(mediumPreset, transform); }
        else if (DIFFICULTY == 3) { Instantiate(hardPreset, transform); }
    }

    private void InitializePuzzles()
    {
        List<int> listOfNodeIds = new();

        for (int i = 0; i < nodes.Length; i++)
        {
            listOfNodeIds.Add(i);
        }
        // loop startuje od 1 ponieważ node o id 0 nie posiada pary.
        for (int puzzleId = 1; puzzleId < nodes.Length; puzzleId++)
        {

            GameObject puzzleInstance = Instantiate(puzzleObject);
            MeshRenderer puzzleMeshRenderer = puzzleInstance.GetComponentInChildren<MeshRenderer>();
            puzzleInstance.GetComponent<PuzzlePiece>().SetId(puzzleId);
            puzzleMeshRenderer.material = puzzleMaterials[puzzleId];

            // Przypisanie puzla do losowego node'a.
            int index = Random.Range(0, listOfNodeIds.Count);
            nodes[listOfNodeIds[index]].SetPuzzlePiece(puzzleInstance.GetComponent<PuzzlePiece>(), GetIsMoving());
            listOfNodeIds.RemoveAt(index);
        }
    }

    private void InitializeNodes()
    {
        int nodeId = 0;
        foreach (GraphNode node in nodes)
        {
            node.SetId(nodeId);
            MeshRenderer nodeMeshRenderer = node.GetComponentInParent<MeshRenderer>();
            nodeMeshRenderer.material = puzzleMaterials[nodeId];
            if (node.IsMatched()) { matchedPuzzles.Append(node); }
            nodeId++;
        }
    }

    public void MakeMove(GraphNode sourceNode)
    {
        if (!sourceNode.IsEmpty() && sourceNode.FindEmptyNeighbour() != null)
        {
            SetIsMoving(true);
            GraphNode emptyNeighbour = sourceNode.FindEmptyNeighbour();
            if (levelState.IsExpertModeOn())
            {
                emptyNeighbour.GetComponentInParent<MeshRenderer>().material.color = Color.grey;
            }
            PuzzlePiece puzzlePieceToMove = sourceNode.RemovePuzzle();
            emptyNeighbour.SetPuzzlePiece(puzzlePieceToMove, GetIsMoving());

            StartCoroutine(MovePuzzleCoroutine(sourceNode, emptyNeighbour, puzzlePieceToMove));

            foreach (GraphNode node in nodes)
            {
                if (node.IsMatched())
                {
                    if (!matchedPuzzles.Contains(node))
                    {
                        matchedPuzzles.Add(node);
                    }
                }
                else
                {
                    if (matchedPuzzles.Contains(node))
                    {
                        matchedPuzzles.Remove(node);
                    }
                }
            }
        }
    }

    private IEnumerator MovePuzzleCoroutine(GraphNode source, GraphNode destination, PuzzlePiece pieceToMove)
    {
        Vector3 sourcePosition = source.transform.position;
        Vector3 destinationPostition = destination.transform.position;
        
        float duration = 1.0f;
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            pieceToMove.transform.position = Vector3.Lerp(
                sourcePosition,
                destinationPostition,
                elapsedTime / duration
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if (elapsedTime >= duration) { pieceToMove.transform.position = destinationPostition; }
        SetIsMoving(false);
    }

    void Update()
    {
        if (nodes.Length - 1 == matchedPuzzles.Count)
            isSolved = true;
        if (isSolved)
        {
            int nodeId = 0;
            foreach (GraphNode node in nodes)
            {
                node.SetId(nodeId);
                MeshRenderer nodeMeshRenderer = node.GetComponentInParent<MeshRenderer>();
                nodeMeshRenderer.material = solvedMaterial;
                nodeId++;
            }
        }
        if (GetNeedChecking() && GetIsSolved())
        {
            levelState.audioManager.PlaySFX(levelState.audioManager.success);
            SolveGame();
        }
    }
}
