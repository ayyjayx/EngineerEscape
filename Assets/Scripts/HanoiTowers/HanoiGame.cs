using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HanoiGame : GameState
{
    int DIFFICULTY = 1;

    [SerializeField] GameObject[] blocks;

    public GameObject startButton;
    public GameObject midButton;
    public GameObject finalButton;

    private HanoiButton startButtonState;
    private HanoiButton midButtonState;
    private HanoiButton finalButtonState;

    Tower source;
    Tower destination;

    float distanceBetweenStacks = 2f;
    float blockYScale;
    public bool isMoving = false;
    [SerializeField] float moveDuration = .3f;

    private class Tower
    {
        public GameObject towerObject;
        private Stack<GameObject> stack = new Stack<GameObject>();
        bool initialized = false;

        public Tower(GameObject tower)
        {
            towerObject = tower;
            initialized = true;
        }

        public void Push(GameObject obj)
        {
            stack.Push(obj);
        }

        public GameObject Pop()
        {
            if (stack.Count > 0)
            {
                return stack.Pop();
            }
            return null;
        }

        public int Count()
        {
            return stack.Count;
        }

        public GameObject Peek()
        {
            if (stack.Count > 0)
            {
                return stack.Peek();
            }
            return null;
        }

        public bool IsInitialized()
        {
            return initialized;
        }

        public static implicit operator bool(Tower tower)
        {
            return tower != null && tower.IsInitialized();
        }
    }

    Tower startTower;
    Tower midTower;
    Tower finalTower;
    

    private void Start()
    {
        DIFFICULTY = levelState.GetDifficulty();

        blockYScale = blocks[0].transform.localScale.y;
        Transform tableTransform = GetComponent<Transform>();

        InitializeStackObjects(tableTransform);

        startButtonState = startButton.GetComponent<HanoiButton>();
        midButtonState = midButton.GetComponent<HanoiButton>();
        finalButtonState = finalButton.GetComponent<HanoiButton>();

        InitializeBaseStack();
    }

    private void InitializeStackObjects(Transform tableTransform)
    {
        startTower = new Tower(new GameObject("startTower"));
        midTower = new Tower(new GameObject("midTower"));
        finalTower = new Tower(new GameObject("finalTower"));

        startTower.towerObject.transform.position = new Vector3(
            tableTransform.position.x,
            tableTransform.position.y + tableTransform.localScale.y + blockYScale / 2,
            tableTransform.position.z + distanceBetweenStacks
        );

        midTower.towerObject.transform.position = new Vector3(
            tableTransform.position.x,
            tableTransform.position.y + tableTransform.localScale.y + blockYScale / 2,
            tableTransform.position.z
        );

        finalTower.towerObject.transform.position = new Vector3(
            tableTransform.position.x,
            tableTransform.position.y + tableTransform.localScale.y + blockYScale / 2,
            tableTransform.position.z - distanceBetweenStacks
        );
    }

    private void InitializeBaseStack()
    {
        for (int i = 0; i < 2 + DIFFICULTY; i++)
        {
            GameObject newInstance = Instantiate(blocks[i]);
            newInstance.transform.position = new Vector3(
                startTower.towerObject.transform.position.x,
                startTower.towerObject.transform.position.y + blockYScale * i,
                startTower.towerObject.transform.position.z
            );
            startTower.Push(newInstance);
        }
    }

    void Update()
    {
        if (!isSolved && !isMoving)
        {
            if (!source)
            {
                source = GetPressedButton();
            }
            else
            {
                destination = GetPressedButton();
                if (destination)
                {
                    if (ValidMove()) StartCoroutine(MoveBrickCoroutine());
                    RestartButtons();
                }
            }
            if (finalTower.Count() == blocks.Count()) SetIsSolved(true);
        }
        if (GetNeedChecking() && GetIsSolved())
        {
            SolveGame();
        }
    }

    private bool ValidMove()
    {
        if (source.Count() == 0) return false;
        else if (destination.Count() == 0) return true;

        GameObject sourceBrick = source.Peek();
        GameObject destinationBrick = destination.Peek();
        if (sourceBrick.transform.localScale.x < destinationBrick.transform.localScale.x) return true;
        else return false;
    }

    private IEnumerator MoveBrickCoroutine()
    {
        isMoving = true;
        GameObject brick = source.Pop();
        List<Vector3> targetPositions = new List<Vector3>
        {
            new(
            brick.transform.position.x,
            destination.towerObject.transform.position.y + 1f,
            brick.transform.position.z
            ),
            new(
            brick.transform.position.x,
            destination.towerObject.transform.position.y + 1f,
            destination.towerObject.transform.position.z
            ),
            new(
            brick.transform.position.x,
            destination.towerObject.transform.position.y + 0.2f * destination.Count(),
            destination.towerObject.transform.position.z
            ),

        };
        destination.Push(brick);
        foreach (Vector3 targetPosition in targetPositions)
        {
            float elapsedTime = 0.0f;

            Vector3 initialPosition = brick.transform.position;
            while (elapsedTime < moveDuration)
            {
                brick.transform.position = Vector3.Lerp(
                    initialPosition,
                    targetPosition,
                    elapsedTime / moveDuration
                );

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        brick.transform.position = targetPositions[^1];
        isMoving = false;
    }

    private void RestartButtons()
    {
        destination = null;
        source = null;
        startButtonState.TurnOff();
        midButtonState.TurnOff();
        finalButtonState.TurnOff();
    }

    private Tower GetPressedButton()
    {
        if (startButtonState.isClicked && source != startTower)
        {
            return startTower;
        }
        else if (midButtonState.isClicked && source != midTower)
        {
            return midTower;
        }
        else if (finalButtonState.isClicked && source != finalTower)
        {
            return finalTower;
        }
        return null;
    }
}
