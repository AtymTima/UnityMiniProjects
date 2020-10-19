using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Power Up Wave Config")]
public class PowerUpWaveConfig : ScriptableObject
{
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] GameObject powerUpPath;

    [SerializeField] float timeBeforeNextSpawn = 25f;
    [SerializeField] float randomizedFactor = 5f;
    [SerializeField] float powerUpMoveSpeed = 2f;

    public GameObject GetPowerUpPrefab() { return powerUpPrefab; }

    public List<Transform> GetPowerUpWaypoints()
    {
        var waypointsPowerUp = new List<Transform>();
        foreach (Transform child in powerUpPath.transform)
        {
            waypointsPowerUp.Add(child);
        }
        return waypointsPowerUp;
    }

    public float GetTimeBeforeNextSpawn() { return timeBeforeNextSpawn + Random.Range(-randomizedFactor, randomizedFactor); }
    public float GetPowerUpMoveSpeed() { return powerUpMoveSpeed; }

}
