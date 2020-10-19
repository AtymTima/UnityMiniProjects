using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCollider : MonoBehaviour
{
    [SerializeField] GameController GameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        GameController.enemyDestroyed += 1;
        GameController.IsLevelComplete();
    }
}
