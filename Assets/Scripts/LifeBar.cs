using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    // You can access to this script even if its private
    [SerializeField]
    private GameObject lifeBar;
    private GameObject[] hearts;
    [SerializeField]
    private int totalHearts;

    // Start is called before the first frame update
    void Start()
    {

        totalHearts = 6;
        hearts = new GameObject [6];

        for(int i = 0; i < 6; i++)
        {
            hearts[i] = lifeBar.transform.GetChild(i).gameObject;
        }
        // heart1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            if (i < totalHearts)
            {
                hearts[i].SetActive(true);
            }
            else 
            { 
                hearts[i].SetActive(false);
            }
        }
    }

    public void DecreaseLife(int hearts)
    {
        totalHearts -= hearts;
    }

    public void IncreaseLife(int hearts)
    {
        totalHearts += hearts;
    }
}
