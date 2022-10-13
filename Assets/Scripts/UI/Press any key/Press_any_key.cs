using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Press_any_key : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey){
            anim.SetBool("AnyKeyWasPressed",true);
            
        }

    }

    private void changeToMenu(){
        SceneManager.LoadScene("Menu");
    }
}
