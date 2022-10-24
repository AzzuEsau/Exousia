using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{

    public AudioSource SonidoMenu;
    public GameObject SonidoDeMenu;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(SonidoDeMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
