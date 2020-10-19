using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    //config params

    float healthPerPowerUp;
    float averageHealthPerPowerUp = 1000;
    float randomizedFactor = 200;

    //cashed reference
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            healthPerPowerUp = GenerateRandomHealth();
            player.AddPlayerHealth((int)healthPerPowerUp * 10);
            Destroy(gameObject);
        }
        if (collision.name == "Player Laser(Clone)")
        {
            Destroy(gameObject);
        }
    }

    private float GenerateRandomHealth()
    {
        return averageHealthPerPowerUp + UnityEngine.Random.Range(-randomizedFactor, randomizedFactor);
    }
}
