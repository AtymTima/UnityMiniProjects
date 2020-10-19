using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    /* Config params */
    [SerializeField] int damage = 100;

    public int GetDamage()
    {
        return damage;
    }

    public void DestroyObject(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void HitLaser(Collider2D otherCollision)
    {
        if (otherCollision.gameObject.tag == "LaserProjectile" || otherCollision.gameObject.tag == "bombProjectile")
        {
            Destroy(gameObject);
        }
    }
}
