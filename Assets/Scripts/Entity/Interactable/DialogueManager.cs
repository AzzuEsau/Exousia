using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//************** agregar para mover botones y texto
using TMPro; // ********* Esto para crear objetos tipo TextMeshPro

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;
    #pragma warning restore 0649

    // We get the text components of dialogue panel
    private TextMeshProUGUI dialogueText, nameText, continueButtonText;
    // We get the button of dialogue panel
    private Button continueButton;

    private List<string> dialogueList;
    private int dialogueID;
    private string name;


    // Start is called before the first frame update
    void Start()
    {
        dialogueText = dialoguePanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        nameText = dialoguePanel.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();

        //We get the third child of dialogue panel (Buttonn)
        continueButton = dialoguePanel.transform.GetChild(3).GetComponent<Button>();
        if (continueButton != null)
        {
            //add listener
            continueButtonText = continueButton.GetComponentInChildren<TextMeshProUGUI>();
            if (continueButtonText != null)
            {
                continueButtonText.text = "Continuar";
            }
        }

        dialoguePanel.SetActive(false);
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
    }

    public void SetDialogue(string _name, string[] dialogue)
    {
        name = _name;
        dialogueList = new List<string>(dialogue.Length);
        dialogueList.AddRange(dialogue);
        dialogueID = 0;
        nameText.text = name;
        continueButtonText.text = "Continuar";

        ShowDialogue();
        dialoguePanel.SetActive(true);
    }

    public void ShowDialogue()
    {
        dialogueText.text = dialogueList[dialogueID];
    }

    public void ContinueDialogue()
    {
        if (dialogueID == dialogueList.Count - 1)//Se termina
        {
            dialoguePanel.SetActive(false);
        }
        else if (dialogueID == dialogueList.Count - 2)//Uno antes de terminar
        {
            continueButtonText.text = "Salir";
            dialogueID++;
            ShowDialogue();
        }
        else
        {
            dialogueID++;
            ShowDialogue();
        }

    }

}
