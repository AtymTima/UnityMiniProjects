using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KniferCollision : MonoBehaviour
{
    [SerializeField] KniferAttack knifer;
    GameObject currentTarget;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            knifer.ChangeState(true);
            currentTarget = collision.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            knifer.ChangeState(false);
            currentTarget = null;
        }
    }

    public void DamageOpponent(float damage)
    {
        if (currentTarget == null) { return; }
        currentTarget.GetComponent<Health>().ReduceHealth(damage);
    }
}
