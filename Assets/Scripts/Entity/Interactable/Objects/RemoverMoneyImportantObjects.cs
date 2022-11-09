using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverMoneyImportantObjects : MonoBehaviour
{

    protected bool decisionWasntTaked = true;

    protected string decisionName;
    protected string decisionResponse;
    [SerializeField]
    protected int DecisionIndex;

    [SerializeField]
    protected DecisionController decisionController;
    [SerializeField]
    protected int money;
    [SerializeField]
    protected MoneyManager moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager _gameManager = FindObjectOfType<GameManager>();
        decisionController = _gameManager.GetDecisionController();

        if(decisionController == null)
            decisionController = FindObjectOfType<DecisionController>();

        StartCoroutine(CheckIfDecisionWasTaked());
    }

    protected void RemoveMoney(int money)
    {
        moneyManager.SetMoney(moneyManager.GetMoney() - money);
    }

    IEnumerator CheckIfDecisionWasTaked(){
        while(decisionWasntTaked){
            decisionName = decisionController.GetDecisionName(DecisionIndex);
            decisionResponse = decisionController.GetDecisionResponse(DecisionIndex);
            decisionWasntTaked = (decisionName == null || decisionName == "");
            yield return new WaitForSeconds(2);
        }
        if(decisionResponse == "yes")
            RemoveMoney(money);
    }
}
