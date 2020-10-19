using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    /*config parametres */
    WaveConfig waveConfig;
     List<Transform> waypoints;
    float enemyMoveSpeed;

    int waypointIndex = 0;

    void Start()
    {
        enemyMoveSpeed = waveConfig.GetEnemySpeed();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;

    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    void Update()
    {
        MoveEnemyToNextWaypoint();
    }

    private void MoveEnemyToNextWaypoint()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementOfThisFrame = enemyMoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementOfThisFrame);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
