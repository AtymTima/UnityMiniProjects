using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    //config params
    [Header("Wave")]
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] bool looping = false;
    int startingWave = 0;

    [Header("Wave Sound")]
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float waveAudioVolume = 0.3f;

    [Header("Boss Sound")]
    [SerializeField] AudioClip bossSpawnSound;
    [SerializeField] float bossAudioVolume = 1f;

    //cashed reference
    SoundVFX soundVFX;

    IEnumerator Start()
    {
        soundVFX = GetComponent<SoundVFX>();
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {

        int waveIndex = UnityEngine.Random.Range(0, waveConfigs.Count - 1);
        var currentWave = waveConfigs[waveIndex];

        if (currentWave.GetEnemyPrefab().tag == "Bomber")
        {
            SpawnBombers(currentWave);
            yield break;
        }
        yield return StartCoroutine(SpawnAllEnemiesInTheWave(currentWave));
    }

    private void PlayWaveAudio()
    {
        AudioClip clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        soundVFX.PlaySound(clip, waveAudioVolume);
    }

    private IEnumerator SpawnAllEnemiesInTheWave(WaveConfig waveConfig)
    {
        int numberOfEnemies = waveConfig.GetNumberOfEnemies();
        for (int enemyCount = 0; enemyCount <= numberOfEnemies - 1; enemyCount++)
        {
            var newEnemy = Instantiate(
                    waveConfig.GetEnemyPrefab(),
                    waveConfig.GetWaypoints()[0].transform.position,
                    Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);


            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    private void SpawnBombers(WaveConfig waveConfig)
    {
        StartCoroutine(SpawnAllEnemiesInTheWave(waveConfig));
    }

    public void SpawnBoss()
    {
        soundVFX.PlaySound(bossSpawnSound, bossAudioVolume);
        var waveBoss = waveConfigs[waveConfigs.Count - 1];
        var newEnemy = Instantiate(
                                waveBoss.GetEnemyPrefab(),
                                waveBoss.GetWaypoints()[0].transform.position,
                                Quaternion.identity);
        newEnemy.GetComponent<BossPathing>().SetWaveConfig(waveBoss);
    }

    //public bool EnemyIsSpawned()
    //{
    //    return true;
    //}

}
