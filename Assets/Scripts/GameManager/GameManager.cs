using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private DecisionController decisionController;
    [SerializeField]
    private KillsManager killsManager;

    private string _notFoundMessage = "El manejador de juego no encontro un ";
    private string _notGotMessage = "El manejador de juego no tiene un ";


    private void Start() {
        decisionController = FindDecisionController();
        killsManager = FindKillsManager();
    }


    public DecisionController FindDecisionController()
    {
        DecisionController component = FindObjectOfType<DecisionController>();
        string compName = "Decision Controller";
        if(component==null)
        {
            Debug.LogWarning(_notFoundMessage + compName);
        }
        return component;
    }

    public DecisionController GetDecisionController()
    {
        if (decisionController == null)
        {
            Debug.LogWarning(_notGotMessage + "Decision Controller");
        }
        return decisionController;
    }

     public KillsManager FindKillsManager()
    {
        KillsManager component = FindObjectOfType<KillsManager>();
        string compName = "Kills Manager";
        if(component==null)
        {
            Debug.LogWarning(_notFoundMessage + compName);
        }
        return component;
    }

    public KillsManager GetKillsManager()
    {
        KillsManager compVar = killsManager;
        string compName = "Kills Manager";
        if (compVar == null)
        {
            Debug.LogWarning(_notGotMessage + compName);
        }
        return compVar;
    }

}
