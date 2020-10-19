using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //config params
    [SerializeField] float myHealth;
    bool isHealthReceived;

    public delegate void OnPlayerDied(GameObject character);
    public static event OnPlayerDied onPlayerDied = delegate { };
    public delegate void OnEnemyDied(GameObject character);
    public static event OnEnemyDied onEnemyDied = delegate { };

    private void OnEnable()
    {
        PlayerSpawner.onDefenderCreated += GetInitialHealthPlayer;
        EnemySpawner.onEnemySpawned += GetInitialHealthEnemy;
    }

    private void OnDisable()
    {
        PlayerSpawner.onDefenderCreated -= GetInitialHealthPlayer;
        EnemySpawner.onEnemySpawned -= GetInitialHealthEnemy;
    }

    private void GetInitialHealthPlayer(int cost, float health)
    {
        UpdateHealthIfSpawned(health);
    }

    private void GetInitialHealthEnemy(float health)
    {
        UpdateHealthIfSpawned(health);
    }

    private void UpdateHealthIfSpawned(float health)
    {
        if (!isHealthReceived)
        {
            myHealth = health;
            isHealthReceived = true;
        }
    }

    public void ReduceHealth(float damage)
    {
        myHealth -= damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (myHealth <= 0)
        {
            //MyAudioManager.instance.PlayAudio("HorseDeath", false, 0, 0);
            if (gameObject?.tag == "Player")
            {
                onPlayerDied?.Invoke(gameObject);
            }
            else if (gameObject?.tag == "Enemy")
            {
                onEnemyDied?.Invoke(gameObject);
            }
            Destroy(gameObject);
        }
    }

}
