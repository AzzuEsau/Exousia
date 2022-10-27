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
    public TMP_Dropdown screenResolutionsDropDown;
    Resolution[] screenResolutions;
    public int languageList;
    public static int languageIndex = 0;
    Transform[] children;
    public GameObject pauseMenu;

    void Start()
    {
        //-------------- Logica de sonido del juego
        slider.value = PlayerPrefs.GetFloat("AudioLevel",0.5f);
        languageIndex = PlayerPrefs.GetInt("languageIndex",0);
        AudioListener.volume = slider.value;
        // CheckIfIsMuted();

        //-------------- Logica de pantalla completa
        if(Screen.fullScreen){
            toggle.isOn = true;
        }else{
            toggle.isOn = false;
        }

        //-------------- Logica de resolution
        CheckResolution();

        ChangeLanguage();
    }

    public void ChangeSlider(float valor){
        sliderValue = valor;
        PlayerPrefs.SetFloat("AudioLevel", sliderValue);
        AudioListener.volume = slider.value;
        CheckIfIsMuted();
    }

    public void CheckIfIsMuted(){
        if(sliderValue == 0){
            imageMute.enabled = true;
        }else{
            imageMute.enabled = false;
        }
    }

    public void ActiveFullScreen(bool fullScreen){
        Screen.fullScreen = fullScreen;
    }


    public void CheckResolution(){
        screenResolutions = Screen.resolutions;
        screenResolutionsDropDown.ClearOptions();
        List<string> options = new List<string>();
        int actualResolution = 0;

        for(int i = 0; i < screenResolutions.Length; i++){
            string opcion = screenResolutions[i].width + " x " + screenResolutions[i].height;
            options.Add(opcion);

            if(Screen.fullScreen && screenResolutions[i].width == Screen.currentResolution.width &&
                screenResolutions[i].height == Screen.currentResolution.height){
                    actualResolution = i;
            }
        }
        screenResolutionsDropDown.AddOptions(options);
        screenResolutionsDropDown.value = actualResolution;
        screenResolutionsDropDown.RefreshShownValue();

        screenResolutionsDropDown.value = PlayerPrefs.GetInt("resolutionIndex",0);
    }

    public void ChangeResolution(int indiceresolution){
        PlayerPrefs.SetInt("resolutionIndex", screenResolutionsDropDown.value);

        Resolution resolution = screenResolutions[indiceresolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ChangeLanguageDer(){
        languageIndex++;

        if(languageIndex > languageList-1) languageIndex = 0;
        else if(languageIndex < 0 ) languageIndex = languageList - 1;

        ChangeLanguage();
    }

    public void ChangeLanguageIzq(){
        languageIndex--;

        if(languageIndex > languageList-1) languageIndex = 0;
        else if(languageIndex < 0 ) languageIndex = languageList - 1;

        ChangeLanguage();
    }

    public void ChangeLanguage(){

    
        pauseMenu.SetActive(false);
        pauseMenu.SetActive(true);
        PlayerPrefs.SetInt("languageIndex",languageIndex);
    }

    public void ExitToMenu(){
        PlayerPrefs.Save();
        SceneManager.LoadScene("PressKey");
    }
}
