using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Text mainText;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        mainText.text = "Wet Text T_T";
        button.onClick.AddListener(ChangeMainText); // Dodajemy obsługę kliknięcia przycisku
    }

    void ChangeMainText()
    {
        mainText.text = "Super Dry Text B)";
    }
}
