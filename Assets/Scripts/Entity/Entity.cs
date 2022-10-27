using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header ("Entity")]
    [SerializeField] protected float life;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float exp;
    [SerializeField] protected string entityName;
    [SerializeField] protected bool hitted;
    [SerializeField] protected float knockBack;
    [SerializeField] protected Rigidbody2D rb;

    protected void DecreaseLife(float decrease) => this.life -= decrease;

    public static explicit operator Entity(Collision2D v)
    {
        throw new NotImplementedException();
    }

    protected IEnumerator KnockBack(float seconds, GameObject target)
    {
        hitted = true;
        Vector2 direction = target.transform.position - transform.position;
            rb.velocity = -(direction.normalized) * movementSpeed * knockBack;
        yield return new WaitForSeconds(seconds);

        hitted = false;
    }

    public virtual bool OnHurt(float damage, GameObject source)
    {
        if(life > 1 )
        {
            DecreaseLife(damage);
            StartCoroutine(KnockBack(.5f, source));
            return true;
        }
        else
            Destroy(gameObject, 0.2f);

        return false;
    }

    

}
