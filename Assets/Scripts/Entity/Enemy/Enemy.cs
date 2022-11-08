using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header ("Enemy")]
        [SerializeField][Range(1, 10)] protected float detectionRaius;
        [SerializeField] protected LayerMask playerLayer;
        [SerializeField] protected Player player;
        [SerializeField] protected DroppableItems droppableItem;
        private bool hittedT = false;
        private KillsManager kills;

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
                StartCoroutine(HurtInSeconds(gameObject.GetComponent<Entity>(), 1));
                hittedT = true;
            }
        }
    }

    protected IEnumerator HurtInSeconds(Entity entity, float damage){
            yield return new WaitForSeconds(1);
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

    public override bool OnHurt(float damage, GameObject source)
    { 
        
        if(life > 0 )
        {
            DecreaseLife(damage);
            if(life == 0)
            {
                GameManager _gameManager = FindObjectOfType<GameManager>();
                kills = _gameManager.GetKillsManager();
                kills.SetKill(1);
                Destroy(gameObject,0.2f);
                if(droppableItem != null){
                    droppableItem.Drop(gameObject.transform.position);
                }
                return true;
            }

            StartCoroutine(KnockBack(.5f, source));
            return true;
        }
        return false;
    }

    protected virtual void Move() { }

}
