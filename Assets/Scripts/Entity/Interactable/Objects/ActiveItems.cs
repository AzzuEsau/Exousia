using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItems : Interactable
{
    [SerializeField]
    private GameObject[] objectsAffected;
    
    [SerializeField]
    private bool notAffectAllObjects;
    
    [SerializeField]
    private int[] indexOfObjectsAffected;

    [SerializeField]
    private bool dissapearInsteadAppear;


    private bool stateObjects;


    private void Start()
    {
        stateObjects = !dissapearInsteadAppear;
        ActivateOrDeactivate(indexOfObjectsAffected);

    }

    public void ActivateOrDeactivate(int[] indexes){
        int j = 0;
        stateObjects = !stateObjects;

        for(int i = 0; i < objectsAffected.Length; i++){
            if(!notAffectAllObjects || (j <= indexes.Length-1 && (indexes[j] == i))){
                objectsAffected[i].SetActive(stateObjects);
                j++;   
            }else{
                objectsAffected[i].SetActive(!stateObjects);
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hola");
        //Enemy Hitted
        if(collision.gameObject.CompareTag("Player"))
        {
            ActivateOrDeactivate(indexOfObjectsAffected);
            Destroy(gameObject, 0.2f);
        }
    }


}
