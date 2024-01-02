using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Vector3 targetPosition;
    public float movementTime = 3f;

    private float elapsedTime;
    private Vector3 startingPosition;
    
    [SerializeField] bool shouldOpen = false;

    public void SetShouldOpen(bool state)
    {
        shouldOpen = state;
        elapsedTime = 0f;
        Debug.Log(transform.position);
        startingPosition = transform.position;
    }

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (shouldOpen)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < movementTime)
            {
                float t = elapsedTime / movementTime;
                transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }
}