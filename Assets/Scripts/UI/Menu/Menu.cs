using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Menu : MonoBehaviour
{
    public GameObject flecha, lista;
    public GameObject OptinsInterface;
    public GameObject ChargeInterface;
    int indice = 0;

    // Start is called before the first frame update
    void Start()
    {
        Dibujar();
    }

    // Update is called once per frame
    void Update()
    {
        bool up = Input.GetKeyDown("up");
        bool down = Input.GetKeyDown("down");

        if(up) indice--;
        if(down) indice++;

        if(indice > lista.transform.childCount-1) indice = 0;
        else if(indice < 0 ) indice = lista.transform.childCount - 1;

        if(up || down) Dibujar();

        if(Input.GetKeyDown("return")) Accion();
    }

    void Dibujar(){
        Transform opcion = lista.transform.GetChild(indice);
        flecha.transform.position = new Vector3(opcion.position.x-2.3f, opcion.position.y+0.2f, 0f);


    }

    void Accion(){
        Transform opcion = lista.transform.GetChild(indice);


        if(opcion.gameObject.name == "Inicio"){
            Inicio();
        }

        if(opcion.gameObject.name == "Cargar"){
            Charge();
        }

        if(opcion.gameObject.name == "Opciones"){
            Charge();
        }

        if(opcion.gameObject.name == "Salir"){
            Exit();
        }
    }

    public void Inicio(){
            SceneManager.LoadScene("Olimpo");
    }

    public void Exit(){
            Application.Quit();
    }

    public void Charge(){
        lista.SetActive(false);
        ChargeInterface.SetActive(true);
        flecha.SetActive(false);
    }

    public void ExitCharge(){
        ChargeInterface.SetActive(false);
        lista.SetActive(true);
        flecha.SetActive(true);
    }

    public void Options(){
        OptinsInterface.SetActive(true);
        lista.SetActive(false);
        flecha.SetActive(false);
    }

    public void ExitOptions(){
        OptinsInterface.SetActive(false);
        lista.SetActive(true);
        flecha.SetActive(true);
    }
}
