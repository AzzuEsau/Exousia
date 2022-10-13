using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_Language : MonoBehaviour
{
    [SerializeField] string[] LanguagesTexts;
    [SerializeField] Text MyTextField;
    int myLanguage = Scr_PauseMenu.indiceLenguaje;


    // Start is called before the first frame update
    void Start()
    {
        MyTextField.text = LanguagesTexts[myLanguage];
    }

    private void OnEnable(){
        UpdateText();
    }

    // Update is called once per frame
    void UpdateText()
    {
        myLanguage = Scr_PauseMenu.indiceLenguaje;
        MyTextField.text = LanguagesTexts[myLanguage];
    }
}
