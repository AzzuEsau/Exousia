using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        entityName = "grounded generico";
        gameObject.name = this.entityName;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitted)
            return;


        Move();
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
}
