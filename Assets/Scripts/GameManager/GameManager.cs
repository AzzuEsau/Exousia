using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{

    private DecisionController decisionController;

    private string _notFoundMessage = "El manejador de juego no encontro un ";
    private string _notGotMessage = "El manejador de juego no tiene un ";


    void Start()
    {
        decisionController = FindDecisionController();
    }

    public DecisionController FindDecisionController()
    {
        DecisionController component = FindObjectOfType<DecisionController>();
        string compName = "Dialogue Manager";
        if(component==null)
        {
            Debug.LogWarning(_notFoundMessage + compName);
        }
        return component;
    }

    public DecisionController GetDecisionController()
    {
        DecisionController compVar = decisionController;
        string compName = "Decision Controller";
        if (compVar == null)
        {
            Debug.LogWarning(_notGotMessage + compName);
        }
        return compVar;
    }

}
