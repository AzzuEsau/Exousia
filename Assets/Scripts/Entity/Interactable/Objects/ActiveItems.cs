using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItems : Interactable
{
    [SerializeField]
    private GameObject[] objectsAffected;

    [SerializeField]
    private bool dissapearInsteadAppear;

    private bool stateObjects;


    private void Start()
    {
        stateObjects = !dissapearInsteadAppear;

        ActivateOrDeactivate();

    }

    public void ActivateOrDeactivate(){
        stateObjects = !stateObjects;

        for(int i = 0; i < objectsAffected.Length; i++){
            objectsAffected[i].SetActive(stateObjects);  
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hola");
        //Enemy Hitted
        if(collision.gameObject.CompareTag("Player"))
        {
            ActivateOrDeactivate();
            Destroy(gameObject, 0.2f);
        }
    }


}
