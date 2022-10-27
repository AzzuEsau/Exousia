using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    public GameObject row, list;
    public GameObject OptinsInterface;
    public GameObject ChargeInterface;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        Draw();
    }

    // Update is called once per frame
    void Update()
    {
        bool up = Input.GetKeyDown("up");
        bool down = Input.GetKeyDown("down");

        if(up) index--;
        if(down) index++;

        if(index > list.transform.childCount-1) index = 0;
        else if(index < 0 ) index = list.transform.childCount - 1;

        if(up || down) Draw();

        if(Input.GetKeyDown("return")) Action();
    }

    void Draw(){
        Transform option = list.transform.GetChild(index);
        row.transform.position = new Vector3(option.position.x-2.3f, option.position.y+0.2f, 0f);


    }

    void Action(){
        Transform option = list.transform.GetChild(index);


        if(option.gameObject.name == "Start"){
            StartGame();
        }

        if(option.gameObject.name == "Charge"){
            Charge();
        }

        if(option.gameObject.name == "Options"){
            Options();
        }

        if(option.gameObject.name == "Salir"){
            Exit();
        }
    }

    public void StartGame(){
            SceneManager.LoadScene("Olympus");
    }

    public void Exit(){
            Application.Quit();
    }

    public void Charge(){
        list.SetActive(false);
        ChargeInterface.SetActive(true);
        row.SetActive(false);
    }

    public void ExitCharge(){
        ChargeInterface.SetActive(false);
        list.SetActive(true);
        row.SetActive(true);
    }

    public void Options(){
        OptinsInterface.SetActive(true);
        list.SetActive(false);
        row.SetActive(false);
    }

    public void ExitOptions(){
        OptinsInterface.SetActive(false);
        list.SetActive(true);
        row.SetActive(true);
    }
}
