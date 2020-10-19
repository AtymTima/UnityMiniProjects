using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseCollider : MonoBehaviour
{
    [SerializeField] Horse horse;
    float damage;
    GameObject currentTarget;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "particlesProjectile")
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            damage = damageDealer.GetDamage();
            damageDealer.DestroyParticlesOnHit(gameObject);
            horse.GetHealthComponent()?.ReduceHealth(damage);
        }

        if (collision.gameObject.tag == "Player")
        {
            horse.ChangeState(true, collision.gameObject);
            currentTarget = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            horse.ChangeState(false, collision.gameObject);
        }
    }

    public void DamageOpponent(float damage)
    {
        if (currentTarget == null) { return;  }
        currentTarget.GetComponent<Health>().ReduceHealth(damage);
    }
}
