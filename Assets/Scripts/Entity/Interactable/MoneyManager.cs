using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//************** agregar para mover botones y texto
using TMPro; // ********* Esto para crear objetos tipo TextMeshPro

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MoneyPanel;
    #pragma warning restore 0649

    // We get the text components of money panel
    private TextMeshProUGUI moneyText;

    private int money;


    // Start is called before the first frame update
    void Start()
    {
        moneyText = MoneyPanel.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetMoney(int _money)
    {
        money = _money;
        moneyText.text = money.ToString();
    }

    public int GetMoney()
    {
        return money;
    }

}
