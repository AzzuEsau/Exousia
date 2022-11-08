using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppableItems : MonoBehaviour
{
    [SerializeField] private GameObject item;

    private void Start()
    {
        item.SetActive(false);
    }

    public void Drop(Vector3 position){
        item.SetActive(true);
        item.transform.position = position;
    }

    
    // IEnumerator CheckIfDecisionWasTaked(){
    //     while(decisionWasntTaked){
    //         decisionName = decisionController.GetDecisionName(DecisionIndex);
    //         decisionResponse = decisionController.GetDecisionResponse(DecisionIndex);
    //         decisionWasntTaked = (decisionName == null);
    //         yield return new WaitForSeconds(2);
    //     }
    //     ActivateOrDeactivate(indexOfObjectsAffected);
    // }


}
