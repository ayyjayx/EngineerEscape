using UnityEngine;

public class HanoiButton : MonoBehaviour
{

    [SerializeField] float interactableDistance = 1f;

    public bool isClicked = false;

    private GameObject player;

    [SerializeField] Material buttonDefaultMaterial;
    [SerializeField] Material buttonClickedMaterial;
    [SerializeField] Material buttonSolvedMaterial;

    private HanoiGame hanoiGame;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    private void Start()
    {
        hanoiGame = GetComponentInParent<HanoiGame>();
    }

    private void OnMouseUpAsButton()
    {
        if (!hanoiGame.GetIsSolved() && !hanoiGame.isMoving)
        {
            if (IsButtonInRange())
            {
                audioManager.PlaySFX(audioManager.test_sfx);
                TurnOn();
            }
        }
    }
    private void Update()
    {

        if (hanoiGame.GetIsSolved())
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    public void Toggle()
    {
        if (!isClicked)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void TurnOff()
    {
        GetComponent<Renderer>().material = buttonDefaultMaterial;
        isClicked = false;
    }

    public void TurnOn()
    {
        GetComponent<Renderer>().material = buttonClickedMaterial;
        isClicked = true;
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
}
