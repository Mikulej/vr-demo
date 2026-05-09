using UnityEngine;
using TMPro;

public class KeypadButton : Interactable
{
    [SerializeField] GameObject keypadGameObject;
    [SerializeField] int number;
    Keypad keypad;

    void Start()
    {
        keypad = keypadGameObject.GetComponent<Keypad>();
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        text.text = number.ToString();
    }

    override public void Interact(){
        keypad.AddNumber(number);
    }
}
