using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    //config params
    [SerializeField] Lose lose;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lose.OnCollisionWithEnemy(collision.gameObject);
    }
}
