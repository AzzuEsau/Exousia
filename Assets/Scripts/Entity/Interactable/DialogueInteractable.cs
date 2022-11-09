using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{

    [SerializeField]
    protected string _name;
    [SerializeField]
    protected string[] _dialogue;
    [SerializeField]
    protected bool isDecision;

    [SerializeField]
    protected int decisionInArr;
    [SerializeField]
    protected int decisionIndex;
    [SerializeField]
    protected string nameDecision;
    [SerializeField]
    protected string parentDecision;
    [SerializeField]
    protected GameObject bubble;

    protected int dialogueID;
    
    protected DialogueManager _dialogueManager;

    protected bool alreadyAnwer;

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        if (_dialogueManager == null)
        {
            Debug.LogWarning("No se encontro un DialogueManager en la escena");
        }

        alreadyAnwer = false;
        if(bubble != null)
            bubble.SetActive(true);
    }

    private void Update(){
        dialogueID = _dialogueManager.GetDialogueID();
        if(bubble != null && dialogueID == _dialogue.Length -1){
            bubble.SetActive(!alreadyAnwer);
        }
    }


    public override void Interact()
    {
        if(alreadyAnwer)
            return;

        _dialogueManager.SetDialogue(_name, _dialogue, decisionInArr, decisionIndex, nameDecision, parentDecision, isDecision);
        //Usar un manejador de di�logos para mostrar los di�logos
        if(bubble != null){
            bubble.SetActive(false);
        }

        alreadyAnwer = true;
    }

}
