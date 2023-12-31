using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HanoiGame : MonoBehaviour
{
    [SerializeField] GameObject[] blocks;

    public GameObject startButton;
    public GameObject midButton;
    public GameObject finalButton;

    private HanoiButton startButtonState;
    private HanoiButton midButtonState;
    private HanoiButton finalButtonState;

    Tower source;
    Tower destination;

    float stacksDistanceZ = 2f;
    public bool isSolved = false;
    public bool isMoving = false;

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
        Transform tf = GetComponent<Transform>();
        InitializeStackObjects(tf);

        startButtonState = startButton.GetComponent<HanoiButton>();
        midButtonState = midButton.GetComponent<HanoiButton>();
        finalButtonState = finalButton.GetComponent<HanoiButton>();

        InitializeBaseStack(tf);
    }

    private void InitializeStackObjects(Transform tf)
    {
        startTower = new Tower(new GameObject("startTower"));
        midTower = new Tower(new GameObject("midTower"));
        finalTower = new Tower(new GameObject("finalTower"));

        startTower.towerObject.transform.position = new Vector3(
            tf.position.x,
            tf.position.y + 1.1f,
            tf.position.z + stacksDistanceZ
        );

        midTower.towerObject.transform.position = new Vector3(
            tf.position.x,
            tf.position.y + 1.1f,
            tf.position.z
        );

        finalTower.towerObject.transform.position = new Vector3(
            tf.position.x,
            tf.position.y + 1.1f,
            tf.position.z - stacksDistanceZ
        );
    }

    private void InitializeBaseStack(Transform tf)
    {
        int tableCount = blocks.Length;
        for (int i = 0; i < tableCount; i++)
        {
            GameObject newInstance = Instantiate(blocks[i]);
            newInstance.transform.position = new Vector3(
                startTower.towerObject.transform.position.x,
                startTower.towerObject.transform.position.y + 0.2f * i,
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
            if (finalTower.Count() == blocks.Count()) isSolved = true;
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
            float duration = 1.0f;
            float elapsedTime = 0.0f;

            Vector3 initialPosition = brick.transform.position;
            while (elapsedTime < duration)
            {
                brick.transform.position = Vector3.Lerp(
                    initialPosition,
                    targetPosition,
                    elapsedTime / duration
                );

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
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
