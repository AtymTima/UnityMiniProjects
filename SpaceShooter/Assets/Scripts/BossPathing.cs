using System.Collections.Generic;
using UnityEngine;

public class BossPathing : MonoBehaviour
{
    /*config parametres */
    WaveConfig waveConfig;
    List<Transform> waypoints;
    float bossSpeed;
    int healthBoss;

    bool targetShiftEnabled = false;
    Vector3 bossTargetPosition;


    int waypointIndex = 0;

    GameSession gameSession;

    void Start()
    {
        bossSpeed = waveConfig.GetEnemySpeed();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;

        gameSession = FindObjectOfType<GameSession>();
        healthBoss = gameObject.GetComponent<Enemy>().health;
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    void Update()
    {
        MoveBossToNextWaypoint();
    }

    private void CheckHealthStatusBar()
    {
        if (healthBoss != gameObject.GetComponent<Enemy>().health)
        {

        }
        healthBoss = gameObject.GetComponent<Enemy>().health;
    }

    private void MoveBossToNextWaypoint()
    {
        gameSession.GetBossPosition(gameObject);


        if (healthBoss != gameObject.GetComponent<Enemy>().health) //Shift When is Hitted
        {
            ShiftBossToTheSide();

            if (transform.position == bossTargetPosition) //when it reached the destination of shift
            {
                FinishTheShift();
            }
        }
        else //Regular Path
        {
            FollowThePathWaypoints();
        }
    }

    private void FollowThePathWaypoints()
    {
        bossTargetPosition = waypoints[waypointIndex].transform.position;
        var movementOfThisFrame = bossSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, bossTargetPosition, movementOfThisFrame);

        if (transform.position == bossTargetPosition)
        {
            waypointIndex = UnityEngine.Random.Range(1, waypoints.Count);
        }
    }

    private void FinishTheShift()
    {
        waypointIndex = UnityEngine.Random.Range(1, waypoints.Count);
        healthBoss = gameObject.GetComponent<Enemy>().health;
        targetShiftEnabled = false;
    }

    private void ShiftBossToTheSide()
    {
        if (!targetShiftEnabled)
        {
            float targetShift;
            if (gameSession.IsCloseToLeftOrRight())
            {
                targetShift = UnityEngine.Random.Range(2.5f, 3.75f);
            }
            else
            {
                targetShift = UnityEngine.Random.Range(-3.75f, -2.5f);
            }
            bossTargetPosition = new Vector3(gameObject.transform.position.x + targetShift, gameObject.transform.position.y, 0);
            targetShiftEnabled = true;
        }
        var movementOfThisFrame = bossSpeed * 3 * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, bossTargetPosition, movementOfThisFrame);


    }
}
