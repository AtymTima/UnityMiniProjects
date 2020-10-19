using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float randomSpawnFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float enemySpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints() 
    {
        var wavePoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            wavePoints.Add(child);
        }
        return wavePoints; 
    }

    public float GetEnemySpeed() { return enemySpeed; }

    public float GetRandomFactor() { return randomSpawnFactor; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

}
