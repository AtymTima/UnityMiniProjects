using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    //config params
    Transform playerPosition;
    float activationTime = 20f;

    //cashed reference
    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    IEnumerator Start()
    {
        playerPosition = player.transform;
        yield return new WaitForSeconds(activationTime);
        Destroy(gameObject);
    }

    void Update()
    {
        FollowPlayerPosition();
    }

    private void FollowPlayerPosition()
    {
        playerPosition.position = player.transform.position;
        transform.position = new Vector2(playerPosition.position.x + 0.5f, playerPosition.position.y + 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
