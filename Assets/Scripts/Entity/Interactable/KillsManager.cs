using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//************** agregar para mover botones y texto
using TMPro; // ********* Esto para crear objetos tipo TextMeshPro

public class KillsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject KillPanel;
    #pragma warning restore 0649

    // We get the text components of money panel
    private TextMeshProUGUI killText;

    [SerializeField]
    private int kill;


    // Start is called before the first frame update
    void Start()
    {
        killText = KillPanel.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update(){
        killText.text = kill.ToString();
    }

    public void SetKill(int _kill)
    {
        kill += _kill;
    }

    public int GetKill()
    {
        return kill;
    }

}
