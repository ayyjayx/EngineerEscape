using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiButton : MonoBehaviour
{
    GameObject player;
    [SerializeField] float interactableDistance = 1f;
    private Material buttonDisabledMaterial;
    public Material buttonEnabledMaterial;
    public bool isClicked = false;
    private HanoiGame hanoiGame;
    

    private void Start() {
        buttonDisabledMaterial = GetComponent<Renderer>().material;
        hanoiGame = GetComponentInParent<HanoiGame>();
    }

    private void OnMouseUpAsButton()
    {
        if (!hanoiGame.GetIsSolved() && !hanoiGame.isMoving)
        {
            if (isButtonInRange())
            {
                TurnOn();
            }
        }
    }
    private void Update() {

        if (hanoiGame.GetIsSolved())
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    public void Toggle()
    {
        if (!isClicked)
        {
            GetComponent<Renderer>().material = buttonEnabledMaterial;
            isClicked = true;
        }
        else
        {
            GetComponent<Renderer>().material = buttonDisabledMaterial;
            isClicked = false;
        }
    }

    public void TurnOff()
    {
        GetComponent<Renderer>().material = buttonDisabledMaterial;
        isClicked = false;
    }

    public void TurnOn()
    {
        GetComponent<Renderer>().material = buttonEnabledMaterial;
        isClicked = true;
    }

    private bool isButtonInRange()
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
