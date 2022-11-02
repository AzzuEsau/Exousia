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
    int decisionInArr;
    [SerializeField]
    int decisionIndex;
    [SerializeField]
    string nameDecision;
    [SerializeField]
    string parentDecision;
    
    private DialogueManager _dialogueManager; // *******************

    private void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>(); //Sirve para singletons
        if (_dialogueManager == null)
        {
            Debug.LogWarning("No se encontro un DialogueManager en la escena");
        }
    }


    public override void Interact()
    {
        // base.Interact();
        
        _dialogueManager.SetDialogue(_name, _dialogue, decisionInArr, decisionIndex, nameDecision, parentDecision);
        //Usar un manejador de di�logos para mostrar los di�logos
    }

}
