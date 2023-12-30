using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingClock : MonoBehaviour
{
    private bool isRotating = false;
    private float rotationLimit = 0f;
    private float rotationAngle = 30f;
    [SerializeField] float interactableDistance = 2f;
    private GameObject player;
    private int currentHour;
    [SerializeField] int offset = 0;
    [SerializeField] int chosenHour = 3;
    private bool isSolved = false;

    private void OnMouseUpAsButton()
    {
        if (!isRotating && IsButtonInRange())
        {
            isRotating = true; // Rozpoczęcie rotacji po kliknięciu
        }
    }

    private void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
            rotationLimit += rotationAngle * Time.deltaTime;

            if (rotationLimit >= rotationAngle)
            {
                isRotating = false;
                rotationLimit = 0f;
                currentHour = int.Parse(System.DateTime.Now.ToString("hh"));
                chosenHour += 1;
                if (chosenHour > 12) chosenHour = 1;
                Debug.Log(chosenHour);
                Debug.Log(currentHour);
                Debug.Log(isSolved);
            }
            if (chosenHour == currentHour + offset) { isSolved = true; }
            else { isSolved = false; }
        }
    }

    private bool IsButtonInRange()
    {
        player = GameObject.FindWithTag("Player");
        Transform buttonPosition = GetComponent<Transform>();
        float distance = Vector3.Distance(player.transform.position, buttonPosition.position);
        if (distance > interactableDistance)
        {
            return false;
        }
        else return true;
    }

    public bool CheckIsSolved() { return isSolved; }
}