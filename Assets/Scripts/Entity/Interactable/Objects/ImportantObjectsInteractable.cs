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

    private DecisionController decisionController;

    private ImportantObjectsManager _importantObjectsManager;

    private void Start()
    {
        GameManager _gameManager = FindObjectOfType<GameManager>();
        decisionController = _gameManager.GetDecisionController();

        _importantObjectsManager = FindObjectOfType<ImportantObjectsManager>();

        if (_importantObjectsManager == null)
        {
            Debug.LogWarning("No se encontro un ImportantObjectsManager en la escena");
        }

    }

    public override void Interact()
    {
        _importantObjectsManager.SetImportantObject(theObject);
        decisionController.SetDecision(DecisionIndex, NameDecision, 1000, responseDecision, ParentDecision);
        Destroy(gameObject, 0.1f);
        
    }


}
