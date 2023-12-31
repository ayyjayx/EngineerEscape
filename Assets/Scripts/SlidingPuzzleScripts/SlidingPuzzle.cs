using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlidingPuzzle : MonoBehaviour
{
    bool isSolved = false;

    [SerializeField] GraphNode[] nodes;                  // Tablica wszystkich wezłów grafu.
    [SerializeField] List<GraphNode> matchedPuzzles;     // Lista dopasowanych puzli.
    [SerializeField] GameObject puzzleObject;
    [SerializeField] List<Material> puzzleMaterials;
    [SerializeField] Material solvedMaterial;
    private bool isMoving = false;

    public bool GetIsSolved() { return isSolved; }
    public bool GetIsMoving() { return isMoving; }

    // Start is called before the first frame update
    void Start()
    {
        nodes = FindObjectsOfType<GraphNode>();
        InitializeNodes();
        InitializePuzzles();
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
            nodes[listOfNodeIds[index]].SetPuzzlePiece(puzzleInstance.GetComponent<PuzzlePiece>(), isMoving);
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
            Debug.Log(nodeId);
            nodeMeshRenderer.material = puzzleMaterials[nodeId];
            if (node.IsMatched()) { matchedPuzzles.Append(node); }
            nodeId++;
        }
    }

    public void MakeMove(GraphNode sourceNode)
    {
        if (!sourceNode.IsEmpty() && sourceNode.FindEmptyNeighbour() != null)
        {
            isMoving = true;
            GraphNode emptyNeighbour = sourceNode.FindEmptyNeighbour();
            PuzzlePiece puzzlePieceToMove = sourceNode.RemovePuzzle();
            emptyNeighbour.SetPuzzlePiece(puzzlePieceToMove, isMoving);

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
        else
        {
            Debug.Log("Nothing to move or no empty neighbours.");
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
        isMoving = false;
    }

    // Update is called once per frame
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
    }
}
