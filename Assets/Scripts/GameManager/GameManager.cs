using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{

    // private UI_Manager _ui_Manager;
    // private PlayerController _playerData;
    // private SaveLoadManager _saveLoadManager;
    // private NPCManager _NPCManager;
    private DecisionController decisionController;

    private string _notFoundMessage = "El manejador de juego no encontro un ";
    private string _notGotMessage = "El manejador de juego no tiene un ";


    void Start()
    {
        decisionController = FindDecisionController();
        // _ui_Manager = FindUI_Manager();
        // _playerData = FindPlayerData();
        // _saveLoadManager = FindSaveLoadManager();
        // _NPCManager = FindNPCManager();
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


    // public UI_Manager FindUI_Manager()
    // {
    //     UI_Manager var = FindObjectOfType<UI_Manager>();
    //     string compName = "UI Manager";
    //     if(var==null)
    //     {
    //         Debug.LogWarning(_notFoundMessage + compName);
    //     }
    //     return var;
    // }
    // public PlayerController FindPlayerData()
    // {
    //     PlayerController var = FindObjectOfType<PlayerController>();
    //     string compName = "PlayerData";
    //     if (var == null)
    //     {
    //         Debug.LogWarning(_notFoundMessage + compName);
    //     }
    //     return var;
    // }
    // public SaveLoadManager FindSaveLoadManager()
    // {
    //     SaveLoadManager var = FindObjectOfType<SaveLoadManager>();
    //     string compName = "SaveLoadManager";
    //     if (var == null)
    //     {
    //         Debug.LogWarning(_notFoundMessage + compName);
    //     }
    //     return var;
    // }
    // public NPCManager FindNPCManager()
    // {
    //     NPCManager var = FindObjectOfType<NPCManager>();
    //     string compName = "NPCManager";
    //     if (var == null)
    //     {
    //         Debug.LogWarning(_notFoundMessage + compName);
    //     }
    //     return var;
    // }

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

    // public UI_Manager GetUI_Manager()
    // {
    //     UI_Manager compVar = _ui_Manager;
    //     string compName = "UI Manager";
    //     if (compVar == null)
    //     {
    //         Debug.LogWarning(_notGotMessage + compName);
    //     }
    //     return compVar;
    // }
    // public PlayerController GetPlayerData()
    // {
    //     PlayerController compVar = _playerData;
    //     string compName = "PlayerData";
    //     if (compVar == null)
    //     {
    //         Debug.LogWarning(_notGotMessage + compName);
    //     }
    //     return compVar;
    // }
    // public SaveLoadManager GetSaveLoadManager()
    // {
    //     SaveLoadManager compVar = _saveLoadManager;
    //     string compName = "SaveLoadManager";
    //     if (compVar == null)
    //     {
    //         Debug.LogWarning(_notGotMessage + compName);
    //     }
    //     return compVar;
    // }
    // public NPCManager GetNPCManager()
    // {
    //     NPCManager compVar = _NPCManager;
    //     string compName = "NPCManager";
    //     if (compVar == null)
    //     {
    //         Debug.LogWarning(_notGotMessage + compName);
    //     }
    //     return compVar;
    // }
}
