using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUp : MonoBehaviour
{
    //config params
    [SerializeField] GameObject playerShieldPrefab;
    float periodOfPowerUp = 10f;
    float multiplySpeedFactor = 2f;

    //cashed reference
    PlayerShield playerShield;
    Player player;

    private void Awake()
    {
        playerShield = FindObjectOfType<PlayerShield>();
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SummonShield();
            Destroy(gameObject);
        }
        if (collision.name == "Player Laser(Clone)")
        {
            Destroy(gameObject);
        }
    }

    private void SummonShield()
    {
        GameObject powerShield = Instantiate(playerShieldPrefab, player.transform.position, Quaternion.identity) as GameObject;
    }
}
