using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fadeout : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer SpRenderer;
    private float transparency = 1;
    private float newTransparency = 1;
    public bool autoActive;
    public GameObject StructureCollitions;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(transparency > newTransparency){
            transparency -= 0.02f;
            SpRenderer.color = new Color (1, 1, 1, transparency); 
        }
        
        if(transparency < newTransparency){
            transparency += 0.02f;
            SpRenderer.color = new Color (1, 1, 1, transparency); 
        }
    }
    

    void OnTriggerStay2D(Collider2D col){

        if(col.gameObject.tag == "Player" && (Input.GetKeyDown(KeyCode.W) || autoActive)){
            // Change the 'color' property of the 'Sprite Renderer'
            newTransparency = 0;
            StructureCollitions.SetActive(true);
        }

        if(col.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.S)){
            // Change the 'color' property of the 'Sprite Renderer'
            newTransparency = 1;
            StructureCollitions.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D col){

        if(col.gameObject.tag == "Player" && autoActive){
            // Change the 'color' property of the 'Sprite Renderer'
            newTransparency = 1;
            StructureCollitions.SetActive(false);
        }
    }
       
}
