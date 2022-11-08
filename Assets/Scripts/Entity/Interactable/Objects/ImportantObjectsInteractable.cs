using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantObjectsInteractable : Interactable
{
   
    [SerializeField]
    private string theObject;

    [SerializeField]
    private string NameDecision;

    [SerializeField]
    private int DecisionIndex;

    [SerializeField]
    private string responseDecision;

    [SerializeField]
    private string ParentDecision;

    [Header("Privado")]
    [SerializeField]
    private DecisionController decisionController;
    [SerializeField]
    private ImportantObjectsManager _importantObjectsManager;


    private void Start()
    {
        _importantObjectsManager = FindObjectOfType<ImportantObjectsManager>();
        GameManager _gameManager = FindObjectOfType<GameManager>();
        decisionController = _gameManager.GetDecisionController();

        if(decisionController == null)
            decisionController = FindObjectOfType<DecisionController>();


        if (_importantObjectsManager == null)
        {
            Debug.LogWarning(gameObject.name + " No se encontro un ImportantObjectsManager en la escena");
        }

    }

    public override void Interact()
    {
        _importantObjectsManager.SetImportantObject(theObject);
        decisionController.SetDecision(DecisionIndex, NameDecision, 1000, responseDecision, ParentDecision);
        Destroy(gameObject, 0.1f);
        
    }


}
