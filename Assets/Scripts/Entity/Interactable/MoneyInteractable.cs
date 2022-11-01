using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyInteractable : Interactable
{

    [SerializeField]
    private int _money;

    private int actualMoney;

    private MoneyManager _moneyManager; // *******************

    private void Start()
    {
        _moneyManager = FindObjectOfType<MoneyManager>(); //Sirve para singletons
        if (_moneyManager == null)
        {
            Debug.LogWarning("No se encontro un MoneyManager en la escena");
        }
    }


    public override void Interact()
    {
        // base.Interact();
        actualMoney = _moneyManager.GetMoney();
        actualMoney = actualMoney + _money;
        
        _moneyManager.SetMoney(actualMoney);
        //Usar un manejador de di�logos para mostrar los di�logos
        Destroy(gameObject);
        
    }

}
