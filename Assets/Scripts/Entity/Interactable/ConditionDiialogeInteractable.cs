using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDiialogeInteractable : DialogueInteractable
{
    [SerializeField]
    protected MoneyManager money;
    [SerializeField]
    protected int condition;
    protected bool allowed;


    public override void Interact()
    {
        if(alreadyAnwer)
            return;

        if(money.GetMoney() >= condition)
        {
            allowed = true;
            base.Interact();
            return;
        }

        allowed = false;
        _dialogueManager.SetDialogueWithOneCondition(_name, _dialogue, decisionInArr, decisionIndex, nameDecision, parentDecision, isDecision, false);
        //Usar un manejador de di�logos para mostrar los di�logos
        if(bubble != null){
            bubble.SetActive(false);
        }
        alreadyAnwer = true;
    }
}
