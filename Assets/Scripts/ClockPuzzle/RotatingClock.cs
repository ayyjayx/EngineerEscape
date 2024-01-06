using UnityEngine;

public class RotatingClock : MonoBehaviour
{
    /*
    Klasa odpowiedzialna za działanie pojedyńczego zegara i monitorowanie
    jego stanu.
    */

    private bool isRotating = false;
    private bool isClockSolved = false;
    private float rotationLimit = 0f;
    private float rotationAngle = 30f;
    private int currentHour;
    private Vector3 initialRotation;
    private GameObject player;

    [SerializeField] int offset = 0;
    [SerializeField] int chosenHour = 1;
    [SerializeField] float interactableDistance = 2f;

    [SerializeField] ClockGame clockGame;

    public bool GetIsClockSolved() { return isClockSolved; }

    private void OnMouseUpAsButton()
    {
        if (!isRotating && IsButtonInRange())
        {
            isRotating = true; // Rozpoczęcie rotacji po kliknięciu.
        }
    }

    private void Start() {
        initialRotation = transform.eulerAngles;
        currentHour = int.Parse(System.DateTime.Now.ToString("hh"));
        UpdateClockState(GetCurrentHourWithOffset());
    }

    private void Update()
    {
        if (isRotating)
        {
            RotateClock();

            if (rotationLimit >= rotationAngle)
            {
                Quaternion targetRotation = Quaternion.Euler(initialRotation.x, initialRotation.y + rotationAngle, initialRotation.z);

                transform.rotation = targetRotation;

                initialRotation = transform.eulerAngles;

                SetNextHour();

                rotationLimit = 0f;
                isRotating = false;

                UpdateClockState(GetCurrentHourWithOffset());
            }
        }
    }

    private void UpdateClockState(int targetHour)
    {
        if (chosenHour == targetHour)
        {
            isClockSolved = true;
            GetComponent<Renderer>().material.color = Color.red;
            clockGame.CheckIsSolved();
        }
        else { isClockSolved = false; }
    }

    private int GetCurrentHourWithOffset()
    {
        currentHour = int.Parse(System.DateTime.Now.ToString("hh"));
        int targetHour = currentHour;
        if (offset != 0) { targetHour = (currentHour + offset) % 12; }

        return targetHour;
    }

    private void SetNextHour()
    {
        chosenHour += 1;
        if (chosenHour > 12) chosenHour = 1;
    }

    private void RotateClock()
    {
        transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
        rotationLimit += rotationAngle * Time.deltaTime;
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

    public bool CheckIsSolved() { return isClockSolved; }
}