using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header ("Enemy")]
        [SerializeField][Range(1, 10)] protected float detectionRaius;
        [SerializeField] protected LayerMask playerLayer;
        [SerializeField] protected Player player;
        private bool hittedT = false;


    protected void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(KnockBack(.5f, other.gameObject));
            Hurt(player, 1);
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Enemy Hitted
        if(collision.gameObject.CompareTag("HitAreaPlayer"))
        {
            if(player.IsAttacking() && !hittedT){
                StartCoroutine(KnockBack(.5f, collision.gameObject));
                StartCoroutine(HurtInSeconds(gameObject.GetComponent<Entity>(), 1));
                hittedT = true;
            }
        }
    }

    protected IEnumerator HurtInSeconds(Entity entity, float damage){
            yield return new WaitForSeconds(1/2);
            hittedT = false;
            Hurt(entity, damage);
    }

    protected virtual void Hurt(Entity entity, float damage)
    {
        entity.OnHurt(damage, this.gameObject);
        StartCoroutine(KnockBack(.5f, entity.gameObject));
    }

    protected virtual bool IsDetected()
    {
        return Distance() < detectionRaius;
    }

    protected virtual float Distance()
    {
        return Vector2.Distance(transform.position, player.GetTransform().position);
    }

    protected virtual void Move() { }
}
