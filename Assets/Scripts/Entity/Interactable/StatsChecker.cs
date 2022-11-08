using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private bool karma;
    [SerializeField]
    private bool kills;
    [SerializeField]
    private bool money;

    [SerializeField]
    private float karmaAmount;
    [SerializeField]
    private int killsAmount;
    [SerializeField]
    private int moneyAmount;

    [SerializeField]
    private GameObject[] objectsAffected;

    [SerializeField]
    private int[] indexOfObjectsAffected;

    [SerializeField]
    private bool dissapearInsteadAppear;

    private bool stateObjects;

    private KarmaBar karmaBar;

    private KillsManager killsManager;

    private MoneyManager moneyManager;

    private bool requirementsCompleted;


    private void Start()
    {
        karmaBar = FindObjectOfType<KarmaBar>();
        killsManager = FindObjectOfType<KillsManager>();
        moneyManager = FindObjectOfType<MoneyManager>();

        stateObjects = dissapearInsteadAppear;
        ActivateOrDeactivate(null);
        
    }

    public void ActivateOrDeactivate(int[] indexes){
        int j=0;

        for(int i = 0; i < objectsAffected.Length; i++){
            if(indexes == null){
                objectsAffected[i].SetActive(stateObjects);
            }else{
                if(j <= indexes.Length-1 && (indexes[j] == i)){
                    if(requirementsCompleted){
                        objectsAffected[i].SetActive(stateObjects);
                    }
                    j++;   
                }else if(requirementsCompleted){
                    objectsAffected[i].SetActive(stateObjects);
                }
            }
        }
    }

    private void CheckRequirements(){
        float karmaCount;
        int killsCount;
        int moneyCount;

        if(karma){
            karmaCount = karmaBar.getKarma();
            requirementsCompleted = (karmaCount >= karmaAmount);
        }
        
        if(kills){
            killsCount = killsManager.GetKill();
            requirementsCompleted = (killsCount >= killsAmount);
        }

        if(money){
            moneyCount = moneyManager.GetMoney();
            requirementsCompleted = (moneyCount >= moneyAmount);
        }

        if(requirementsCompleted){
            stateObjects = !stateObjects;
        }

        ActivateOrDeactivate(indexOfObjectsAffected);
        
        if(requirementsCompleted){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("HitAreaPlayer"))
        {
            CheckRequirements();
        }
    }

}