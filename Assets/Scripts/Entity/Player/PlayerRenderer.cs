using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rgBody;
    [SerializeField] private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AssignElements(ref bool isGrounded)
    {

    }

    //make the movement positive and compare it with 0 Epsilon
    public bool IsRunning() => Mathf.Abs(rgBody.velocity.x) > Mathf.Epsilon;
    public bool IsJumping() => Mathf.Abs(rgBody.velocity.y) > Mathf.Epsilon;

 

    // Update is called once per frame
    void Update()
    {
        
    }
}
