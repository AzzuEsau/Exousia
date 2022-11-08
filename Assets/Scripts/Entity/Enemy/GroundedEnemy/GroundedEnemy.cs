using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemy : Enemy
{
    [SerializeField] 
    protected Animator animator;
    private Rigidbody2D rgBody;

    // Start is called before the first frame update
    void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        entityName = "grounded generico";
        gameObject.name = this.entityName;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitted)
            return;

        FlipSprite();
        Move();
        animator.SetBool("isRunning", IsRunning());
    }


    protected override void Move()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        if(IsDetected())
            rb.velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    protected virtual void Jump()
    {
        
    }

    #region Run
    //make the movement positive and compare it with 0 Epsilon
    private bool IsRunning() => Mathf.Abs(rgBody.velocity.x) > Mathf.Epsilon;

    private void FlipSprite()
    {
        if (IsRunning())
        {
            //Rotate de player using sign wich retur if the value is positive or negative
            transform.localScale = new Vector2(Mathf.Sign(rgBody.velocity.x), 1f);
        }
    }
    #endregion
}
