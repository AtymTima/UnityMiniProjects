using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    //config params
    [SerializeField] LoseCollider loseCollider;

    public delegate void OnBaseTriggered(float damageBase);
    public static event OnBaseTriggered onBaseTriggered = delegate { };

    internal void OnCollisionWithEnemy(GameObject collidedEnemy)
    {
        onBaseTriggered?.Invoke(collidedEnemy.GetComponent<DamageDealer>().GetBaseDamage());
    }
}
