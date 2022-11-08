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

    private DecisionController decisionController;

    private GameObject continueGameObj, exitGameObj, acceptGameObj, declineGameObj;
    // We get the text components of dialogue panel
    private TextMeshProUGUI dialogueText, nameText;
    // We get the button of dialogue panel
    private Button continueButton, exitButton, acceptButton, declineButton;

    private List<string> dialogueList;
    private int dialogueID;
    private string nameNPC;

    int decisionInArr;
    int decisionIndex;
    string nameDecision;
    string parentDecision;
    int seconds;
    bool decisionActive;
    string response;
    bool isDecision;

    // Start is called before the first frame update
    void Start()
    {
        GameManager _gameManager = FindObjectOfType<GameManager>();
        decisionController = _gameManager.GetDecisionController();
        
        nameText = dialoguePanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        dialogueText = dialoguePanel.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>();

        //We get the third child of dialogue panel (Button)
        continueGameObj = dialoguePanel.transform.GetChild(3).gameObject;
        exitGameObj = dialoguePanel.transform.GetChild(4).gameObject;
        acceptGameObj = dialoguePanel.transform.GetChild(5).gameObject;
        declineGameObj = dialoguePanel.transform.GetChild(6).gameObject;

        continueButton = continueGameObj.GetComponent<Button>();
        exitButton = exitGameObj.GetComponent<Button>();
        acceptButton = acceptGameObj.GetComponent<Button>();
        declineButton = declineGameObj.GetComponent<Button>();

        if (continueButton != null)
        {
            selectButtonsActive("continue");
        }

        dialoguePanel.SetActive(false);
        exitButton.onClick.AddListener(delegate { ContinueDialogue(); });
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        acceptButton.onClick.AddListener(delegate { Accept(); });
        declineButton.onClick.AddListener(delegate { Reject(); });

    }

    public int GetDialogueID(){ return dialogueID; }

    public void SetDialogue(string nameNPC, string[] dialogue, int decisionInArr, int decisionIndex, string nameDecision, string parentDecision, bool isDecision)
    {
        nameText.text = nameNPC;
        dialogueList = new List<string>(dialogue.Length);
        dialogueList.AddRange(dialogue);
        dialogueID = 0;
        selectButtonsActive("continue");

        this.decisionInArr = decisionInArr;
        this.nameDecision = nameDecision;
        this.decisionIndex = decisionIndex;
        this.parentDecision = parentDecision;
        this.isDecision = isDecision;
        

        ShowDialogue();
        dialoguePanel.SetActive(true);
    }

    public void ShowDialogue()
    {
        dialogueText.text = dialogueList[dialogueID];
    }

    public void Accept(){
        response = "yes";
        ContinueDialogue();
    }

    public void Reject(){
        response = "no";
        ContinueDialogue();
    }

    public void ContinueDialogue()
    {
        if (isDecision && dialogueID == decisionInArr){
            decisionController.SetDecision(decisionIndex, nameDecision, seconds * 1000, response, parentDecision);
            decisionActive = false;
        }


        if (dialogueID == dialogueList.Count - 1)//Se termina
        {
            dialoguePanel.SetActive(false);
        }
        else if (dialogueID == dialogueList.Count - 2)//Uno antes de terminar
        {
            selectButtonsActive("exit");
            dialogueID++;
            ShowDialogue();
        }
        else if (isDecision && dialogueID == decisionInArr - 1){ // Uno antes de la decision
            selectButtonsActive("decision");
            seconds = 0;
            decisionActive = true;
            StartCoroutine(SecondsOfDecision());
            dialogueID++;
            ShowDialogue();
        }
        else
        {
            selectButtonsActive("continue");
            dialogueID++;
            ShowDialogue();
        }

    }

    private void selectButtonsActive(string config){
        continueGameObj.SetActive(config == "continue");
        exitGameObj.SetActive(config == "exit");
        acceptGameObj.SetActive(config == "decision");
        declineGameObj.SetActive(config == "decision");
    }


    IEnumerator SecondsOfDecision(){
        while(decisionActive){
            yield return new WaitForSeconds(1);
            seconds++;
        }

    }
}
