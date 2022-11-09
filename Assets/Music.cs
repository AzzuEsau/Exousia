using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : GenericSingletonClass<Music>
{  
    AudioSource audio;
    [SerializeField]
    AudioClip castle;
    [SerializeField]
    AudioClip battle;
    [SerializeField]
    AudioClip olimpo;

    private void Start() {
        audio = this.GetComponent<AudioSource>();
    }

    private void Update() {
        Scene name = SceneManager.GetActiveScene();

        switch(name.name)
        {
            case "Login": 
                audio.clip = castle;
            break;
            case "MainMenu": 
                audio.clip = castle;
            break;
            case "Credits": 
                audio.clip = castle;
            break;
            case "Controls": 
                audio.clip = castle;
            break;
            case "level_0": 
                audio.clip = battle;
            break;
            case "Olympus": 
                audio.clip = olimpo;
            break;
            default:
                audio.clip = battle;
            break;
        }

        if(!audio.isPlaying)
            audio.Play();  
    }

}
