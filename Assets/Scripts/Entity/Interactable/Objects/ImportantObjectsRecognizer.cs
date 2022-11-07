using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantObjectsRecognizer : Interactable
{
    [SerializeField]
    private GameObject[] objectsAffected;

    [SerializeField]
    private int[] indexOfObjectsAffected;

    [SerializeField]
    private bool dissapearInsteadAppear;

    private bool stateObjects;

    private bool decisionWasntTaked = true;

    private string decisionName;
    private string decisionResponse;

    [SerializeField]
    private int DecisionIndex;

    private DecisionController decisionController;

    private ImportantObjectsManager _importantObjectsManager;


    private void Start()
    {
        stateObjects = !dissapearInsteadAppear;
        int[] array = null;

        ActivateOrDeactivate(array);

        GameManager _gameManager = FindObjectOfType<GameManager>();
        decisionController = _gameManager.GetDecisionController();
        _importantObjectsManager = FindObjectOfType<ImportantObjectsManager>();
        if (_importantObjectsManager == null)
        {
            Debug.LogWarning("No se encontro un ImportantObjectsManager en la escena");
        }
        Debug.Log("Coroutine started");
        StartCoroutine(CheckIfDecisionWasTaked());

    }

    public void ActivateOrDeactivate(int[] indexes){
        stateObjects = !stateObjects;
        int j=0;

        for(int i = 0; i < objectsAffected.Length; i++){
            if(indexes == null || (indexes[j] == i)){
                if(decisionResponse == "yes"){
                    objectsAffected[i].SetActive(stateObjects);
                }
                j++;   
            }else if(decisionResponse == "no"){
                objectsAffected[i].SetActive(stateObjects);
            }
        }
    }

    
    IEnumerator CheckIfDecisionWasTaked(){
        while(decisionWasntTaked){
            // decisionName = decisionController.GetDecisionName(DecisionIndex);
            // decisionResponse = decisionController.GetDecisionResponse(DecisionIndex);
            // decisionWasntTaked = (decisionName != "");
            yield return new WaitForSeconds(2);
            // Debug.Log("checked");
        }
        ActivateOrDeactivate(indexOfObjectsAffected);
    }


}
