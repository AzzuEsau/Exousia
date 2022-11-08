using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//************** agregar para mover botones y texto
using TMPro; // ********* Esto para crear objetos tipo TextMeshPro

public class ImportantObjectsManager : MonoBehaviour
{
    [SerializeField]

    // We get the text components of money panel
    private GameObject Trident, Glasses, GoldenApple, RippleHook;

    Dictionary<string, bool> ImportantObjects = 
        new Dictionary<string, bool>() { 
            {"Trident", false}, 
            {"Glasses",false} ,
            {"GoldenApple", false}, 
            {"RippleHook",false} ,
        };



   

    // the dictionary set false the appearence of object in the interface of important objects until it be true
    void Update(){
        Trident.SetActive(ImportantObjects["Trident"]);
        Glasses.SetActive(ImportantObjects["Glasses"]);
        GoldenApple.SetActive(ImportantObjects["GoldenApple"]);
        RippleHook.SetActive(ImportantObjects["RippleHook"]);
    }

    public void SetImportantObject(string theObject)
    {
        ImportantObjects[theObject] = true;
    }

    public bool GetImportantObject(string theObject)
    {
        return ImportantObjects[theObject];
    }

}
