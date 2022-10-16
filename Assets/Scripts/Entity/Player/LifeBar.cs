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
    private int maxHearts = 6;

    // Start is called before the first frame update
    void Start()
    {

        totalHearts = maxHearts;
        hearts = new GameObject [maxHearts];

        for(int i = 0; i < maxHearts; i++)
        {
            hearts[i] = lifeBar.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxHearts; i++)
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

    public int GetLife()
    {
        return totalHearts;
    }
}
