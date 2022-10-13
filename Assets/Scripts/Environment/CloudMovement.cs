using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float Velocidad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      transform.position += new Vector3(Velocidad * Time.deltaTime, 0, 0);  
    }

    void OnBecameInvisible(){
        Vector3 reset;

        reset = new Vector3(35, 0, 0);

        transform.position += reset;
    }
}
