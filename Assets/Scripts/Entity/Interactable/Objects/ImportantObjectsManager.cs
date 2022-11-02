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



    // Start is called before the first frame update
    void Start()
    {
        Trident = this.gameObject.transform.GetChild(0).gameObject;
        Glasses = this.gameObject.transform.GetChild(1).gameObject;
        GoldenApple = this.gameObject.transform.GetChild(2).gameObject;
        RippleHook = this.gameObject.transform.GetChild(3).gameObject;
    }

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
