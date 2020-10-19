using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Wave")]
    [SerializeField] List<PowerUpWaveConfig> waveConfigs;
    [SerializeField] bool looping;

    float randomTimeBeforeSpawn;

    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnPowerUp());
        }
        while (looping);
    }

    private IEnumerator SpawnPowerUp()
    {
        int waveIndex = Random.Range(0, waveConfigs.Count - 1);
        var currentWave = waveConfigs[waveIndex];
        randomTimeBeforeSpawn = currentWave.GetTimeBeforeNextSpawn();
        yield return new WaitForSeconds(randomTimeBeforeSpawn);

        var powerUp = Instantiate(currentWave.GetPowerUpPrefab(),
                                         currentWave.GetPowerUpWaypoints()[0].transform.position, 
                                         Quaternion.identity);
        powerUp.GetComponent<PowerUpPathing>().SetWaveConfig(currentWave);

    }
}
