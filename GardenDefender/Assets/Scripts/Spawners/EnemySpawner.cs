using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public delegate void OnEnemySpawned(float health);
    public static event OnEnemySpawned onEnemySpawned = delegate { };

    [SerializeField] float averageTimeBetweenSpawns = 12f;
    [SerializeField] List<Attacker> enemyAttackers;
    [SerializeField] GameController GameController;

    float randomFactor = 18f;
    int currentEnemyIndex;
    bool isSpawnTurnedOn;

    private void OnEnable()
    {
        Health.onEnemyDied += IncrementEnemyDestroyed;
    }

    private void OnDisable()
    {
        Health.onEnemyDied -= IncrementEnemyDestroyed;
    }

    IEnumerator Start()
    {
        while (isSpawnTurnedOn == false)
        {
            float timeBeforeSpanw = Random.Range(7, 40);
            isSpawnTurnedOn = true;
            yield return new WaitForSeconds(timeBeforeSpanw);
        }

        while (isSpawnTurnedOn && !GameController.isTimerStopped)
        {
            currentEnemyIndex = Random.Range(0, enemyAttackers.Count - 1);
            var currentAttacker = enemyAttackers[currentEnemyIndex];
            GameObject attacker = Instantiate(currentAttacker.GetEnemyPrefab(), transform.position, Quaternion.identity);
            attacker.transform.parent = transform;

            onEnemySpawned?.Invoke(currentAttacker.GetEnemyHealth());
            GameController.enemyCount += 1;

            float timeBetweenSpawns = averageTimeBetweenSpawns + Random.Range(0, randomFactor);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private void IncrementEnemyDestroyed(GameObject character)
    {
        int numberOfExistingSpawners = GameController.numberOfSpawnersStart - GameController.numberOfSpawners;
        if (numberOfExistingSpawners == 0)
        {
            GameController.enemyDestroyed += 1;
            GameController.numberOfSpawners -= 1;
        }
        else if (numberOfExistingSpawners < GameController.numberOfSpawnersStart - 1)
        {
            GameController.numberOfSpawners -= 1;
        }
        else
        {
            GameController.numberOfSpawners = GameController.numberOfSpawnersStart;
        }
        GameController.IsLevelComplete();
    }
}
