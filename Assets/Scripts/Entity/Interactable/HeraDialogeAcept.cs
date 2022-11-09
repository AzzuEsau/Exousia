using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeraDialogeAcept : DialogueInteractable
{
    protected bool allowed;


    public override void Interact()
    {
        if(alreadyAnwer)
            return;


        _dialogueManager.SetDialogueWithOneCondition(_name, _dialogue, decisionInArr, decisionIndex, nameDecision, parentDecision, isDecision, true);
        //Usar un manejador de di�logos para mostrar los di�logos
        if(bubble != null){
            bubble.SetActive(false);
        }

        alreadyAnwer = true;
    }
}
