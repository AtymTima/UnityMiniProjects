using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPathing : MonoBehaviour
{
    //config params
    PowerUpWaveConfig powerUpWave;
    float powerUpMoveSpeed;
    List<Transform> waypoints;
    Vector3 powerUpTargetPosition;
    int waveIndex;

    void Start()
    {
        powerUpMoveSpeed = powerUpWave.GetPowerUpMoveSpeed();
        waypoints = powerUpWave.GetPowerUpWaypoints();
        transform.position = waypoints[waveIndex].transform.position;
    }

    public void SetWaveConfig(PowerUpWaveConfig powerUpWave)
    {
        this.powerUpWave = powerUpWave;
    }

    void Update()
    {
        MovePowerUpToNextPoint();
    }

    private void MovePowerUpToNextPoint()
    {
        if (waveIndex <= waypoints.Count - 1)
        {
            powerUpTargetPosition = waypoints[waveIndex].transform.position;
            var movementSpeedPerFrame = powerUpMoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, powerUpTargetPosition, movementSpeedPerFrame);

            if (transform.position == powerUpTargetPosition)
            {
                waveIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
