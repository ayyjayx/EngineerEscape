using UnityEngine;
using TMPro;

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

    [SerializeField] bool rootClock; // Zegar na bez przesunięcia.
    [SerializeField] int offset = 0;
    [SerializeField] int chosenHour = 1;
    [SerializeField] float interactableDistance = 2f;

    [SerializeField] TMP_Text correspondingMapText;
    [SerializeField] TMP_Text chosenHourText;

    [SerializeField] ClockGame clockGame;

    public bool IsRootClock() { return rootClock; }
    public void SetOffset(int value) { offset = value; }
    public bool GetIsClockSolved() { return isClockSolved; }
    public void SetCorrespondingMapText(string newText) { correspondingMapText.text = newText; }

    private void OnMouseUpAsButton()
    {
        if (!isRotating && IsButtonInRange())
        {
            isRotating = true; // Rozpoczęcie rotacji po kliknięciu.
        }
    }

    private void Awake() {
        chosenHourText.text = chosenHour.ToString();
        initialRotation = transform.eulerAngles;
        currentHour = int.Parse(System.DateTime.Now.ToString("hh"));
        if(IsRootClock()) { correspondingMapText.text = "0"; }

        // UpdateClockState(GetCurrentHourWithOffset());
    }

    private void Update()
    {
        if (isRotating)
        {
            RotateClock();

            if (rotationLimit >= rotationAngle)
            {
                AdjustRotation();

                SetNextHour();

                rotationLimit = 0f;
                isRotating = false;

                UpdateClockState(GetCurrentHourWithOffset());
            }
        }
    }

    private void AdjustRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(initialRotation.x, initialRotation.y + rotationAngle, initialRotation.z);
        transform.rotation = targetRotation;
        initialRotation = transform.eulerAngles;
    }

    public void UpdateClockState(int targetHour)
    {
        if (chosenHour == targetHour)
        {
            isClockSolved = true;
            clockGame.CheckIsSolved();
        }
        else { isClockSolved = false; }
    }

    public int GetCurrentHourWithOffset()
    {
        currentHour = int.Parse(System.DateTime.Now.ToString("hh"));
        int targetHour = currentHour;
        if (!IsRootClock())
        {
            targetHour = (currentHour + offset) % 12;
            if (targetHour <= 0)
            {
                targetHour += 12;
            }
        }

        return targetHour;
    }

    private void SetNextHour()
    {
        chosenHour += 1;
        if (chosenHour > 12) chosenHour = 1;
        if (clockGame.levelState.IsExpertModeOn())
        {
            chosenHourText.text = "";
        }
        else { chosenHourText.text = chosenHour.ToString(); }
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