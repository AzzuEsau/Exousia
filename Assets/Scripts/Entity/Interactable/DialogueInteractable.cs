using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{

    [SerializeField]
    private string _name;
    [SerializeField]
    private string[] _dialogue;
    [SerializeField]
    private bool isDecision;

    [SerializeField]
    private int decisionInArr;
    [SerializeField]
    private int decisionIndex;
    [SerializeField]
    private string nameDecision;
    [SerializeField]
    private string parentDecision;
    [SerializeField]
    private GameObject bubble;

    private int dialogueID;
    
    private DialogueManager _dialogueManager;

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        if (_dialogueManager == null)
        {
            Debug.LogWarning("No se encontro un DialogueManager en la escena");
        }

        if(bubble != null)
            bubble.SetActive(true);
    }

    private void Update(){
        dialogueID = _dialogueManager.GetDialogueID();
        if(bubble != null && dialogueID == _dialogue.Length -1){
            bubble.SetActive(true);
        }
    }


    public override void Interact()
    {
        
        _dialogueManager.SetDialogue(_name, _dialogue, decisionInArr, decisionIndex, nameDecision, parentDecision, isDecision);
        //Usar un manejador de di�logos para mostrar los di�logos
        if(bubble != null){
            bubble.SetActive(false);
        }
    }

}
