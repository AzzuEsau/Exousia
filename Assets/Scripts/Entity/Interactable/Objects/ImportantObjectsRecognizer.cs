using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantObjectsRecognizer : Interactable
{
    [SerializeField]
    protected GameObject[] objectsAffected;

    [SerializeField]
    protected int[] indexOfObjectsAffected;

    [SerializeField]
    protected bool dissapearInsteadAppear;

    protected bool stateObjects;

    protected bool decisionWasntTaked = true;

    protected string decisionName;
    protected string decisionResponse;

    [SerializeField]
    protected bool notAffectAllAtStart;

    [SerializeField]
    protected int DecisionIndex;

    [SerializeField]
    protected DecisionController decisionController;
    [SerializeField]
    protected ImportantObjectsManager _importantObjectsManager;


    private void Start()
    {
        stateObjects = !dissapearInsteadAppear;
        int[] array = notAffectAllAtStart ? indexOfObjectsAffected : null;

        ActivateOrDeactivate(array);

        GameManager _gameManager = FindObjectOfType<GameManager>();
        decisionController = _gameManager.GetDecisionController();
        _importantObjectsManager = FindObjectOfType<ImportantObjectsManager>();

        if(decisionController == null)
            decisionController = FindObjectOfType<DecisionController>();

        if (_importantObjectsManager == null)
        {
            Debug.LogWarning("No se encontro un ImportantObjectsManager en la escena");
        }
        StartCoroutine(CheckIfDecisionWasTaked());

    }

    public void ActivateOrDeactivate(int[] indexes){
        stateObjects = !stateObjects;
        int j=0;

        for(int i = 0; i < objectsAffected.Length; i++){
            if(indexes == null){
                objectsAffected[i].SetActive(stateObjects);
            }else{
                if(j <= indexes.Length-1 && (indexes[j] == i)){
                    if(decisionResponse == "yes"){
                        objectsAffected[i].SetActive(stateObjects);
                    }
                    j++;   
                }else if(decisionResponse == "no"){
                    objectsAffected[i].SetActive(stateObjects);
                }
            }
        }
    }

    
    IEnumerator CheckIfDecisionWasTaked(){
        while(decisionWasntTaked){
            decisionName = decisionController.GetDecisionName(DecisionIndex);
            decisionResponse = decisionController.GetDecisionResponse(DecisionIndex);
            decisionWasntTaked = (decisionName == null || decisionName == "");
            yield return new WaitForSeconds(2);
        }
        ActivateOrDeactivate(indexOfObjectsAffected);
    }


}
