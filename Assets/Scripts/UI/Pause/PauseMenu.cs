using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class Scr_PauseMenu : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imageMute;
    public Toggle toggle;  
    public TMP_Dropdown resolucionesDropDown;
    Resolution[] resoluciones;
    public int listaLenguajes;
    public static int indiceLenguaje = 0;
    Transform[] children;
    public GameObject pauseMenu;

    void Start()
    {
        //-------------- Logica de sonido del juego
        slider.value = PlayerPrefs.GetFloat("volumenAudio",0.5f);
        indiceLenguaje = PlayerPrefs.GetInt("indiceLenguaje",0);
        AudioListener.volume = slider.value;
        // RevisarSiEstoyMute();

        //-------------- Logica de pantalla completa
        if(Screen.fullScreen){
            toggle.isOn = true;
        }else{
            toggle.isOn = false;
        }

        //-------------- Logica de Resolucion
        RevisarResolucion();

        CambiarLenguaje();
    }

    public void ChangeSlider(float valor){
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute(){
        if(sliderValue == 0){
            imageMute.enabled = true;
        }else{
            imageMute.enabled = false;
        }
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta){
        Screen.fullScreen = pantallaCompleta;
    }


    public void RevisarResolucion(){
        resoluciones = Screen.resolutions;
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for(int i = 0; i < resoluciones.Length; i++){
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);

            if(Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height){
                    resolucionActual = i;
            }
        }
        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();

        resolucionesDropDown.value = PlayerPrefs.GetInt("numeroResolucion",0);
    }

    public void CambiarResolucion(int indiceResolucion){
        PlayerPrefs.SetInt("numeroResolucion", resolucionesDropDown.value);

        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }

    public void CambiarLenguajeDer(){
        indiceLenguaje++;

        if(indiceLenguaje > listaLenguajes-1) indiceLenguaje = 0;
        else if(indiceLenguaje < 0 ) indiceLenguaje = listaLenguajes - 1;

        CambiarLenguaje();
    }

    public void CambiarLenguajeIzq(){
        indiceLenguaje--;

        if(indiceLenguaje > listaLenguajes-1) indiceLenguaje = 0;
        else if(indiceLenguaje < 0 ) indiceLenguaje = listaLenguajes - 1;

        CambiarLenguaje();
    }

    public void CambiarLenguaje(){

    
        pauseMenu.SetActive(false);
        pauseMenu.SetActive(true);
        PlayerPrefs.SetInt("indiceLenguaje",indiceLenguaje);
    }

    public void SalirAlMenu(){
        PlayerPrefs.Save();
        SceneManager.LoadScene("PressKey");
    }
}
