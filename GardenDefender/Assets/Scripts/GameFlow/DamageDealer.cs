using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damagePerAttack;
    [SerializeField] float damageForBase;

    public float GetDamage()
    {
        return damagePerAttack;
    }

    public float GetBaseDamage()
    {
        return damageForBase;
    }

    public void DestroyParticlesOnHit(GameObject collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
