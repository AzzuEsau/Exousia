using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField] protected float floatSpeed;
    protected bool isSuspending = false;

    // Start is called before the first frame update
    void Start()
    {
        entityName = "flying generico";
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
        Vector2 direction = player.transform.position - transform.position;

        if(IsDetected())
            rb.velocity = direction.normalized * movementSpeed;
        else
            Suspend();
            
    }

    protected virtual void Suspend()
    {
        if(isSuspending)
            return;
        
        if (rb.velocity.y <= 0)
            StartCoroutine(fly(.5f, floatSpeed));
        else
            StartCoroutine(fly(.25f, -floatSpeed * 2));
    }


    protected IEnumerator fly(float seconds, float direction)
    {
        isSuspending = true;
        rb.velocity = new Vector2(0, direction);
        
        yield return new WaitForSeconds(seconds);

        isSuspending = false;
    }
}
