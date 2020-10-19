using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelUp : MonoBehaviour
{
    //config params
    float periodOfPowerUp = 10f;
    float multiplySpeedFactor = 2f;

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
            player.GetTurrelUpSettings(periodOfPowerUp, multiplySpeedFactor);
            Destroy(gameObject);
        }
        if (collision.name == "Player Laser(Clone)")
        {
            Destroy(gameObject);
        }
    }

}
