using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCollision : MonoBehaviour
{
    [SerializeField] Runner runner;
    float damage;
    GameObject currentTarget;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "particlesProjectile")
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            damage = damageDealer.GetDamage();
            damageDealer.DestroyParticlesOnHit(gameObject);
            runner.GetHealthComponent().ReduceHealth(damage);
        }

        if (collision.gameObject.tag == "Player")
        {
            runner.ChangeState(true, collision.gameObject);
            currentTarget = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            runner.ChangeState(false, collision.gameObject);
        }
    }

    public void DamageOpponent(float damage)
    {
        if (currentTarget == null) { return; }
        currentTarget.GetComponent<Health>().ReduceHealth(damage);
    }
}
