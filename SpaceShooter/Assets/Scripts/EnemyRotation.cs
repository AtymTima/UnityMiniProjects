using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    GameObject targetPlayer;

    [SerializeField] float speedOfRotation = 360;

    float angleToPlayerDeg;

    private void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (targetPlayer != null)
        {
            CheckPositionOfPlayer();
        }
    }

    private void CheckPositionOfPlayer()
    {
        Vector2 directionToPlayer = FindDirectionToPlayer();
        angleToPlayerDeg = FindAngleToPlayerDegree(directionToPlayer);
        transform.rotation = GetEnemyRotation();
        //transform.rotation = Quaternion.AngleAxis(angleToPlayerDeg + 90f, transform.forward);
    }

    private float FindAngleToPlayerDegree(Vector2 directionToPlayer)
    {
        return Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
    }

    private Vector2 FindDirectionToPlayer()
    {
        if (targetPlayer != null)
        {
            var directionToPlayer = targetPlayer.transform.position - transform.position;
            directionToPlayer.Normalize();
            return directionToPlayer;
        }
        return transform.position;
    }

    public float GetEnemyAngleY()
    {
        Vector2 directionToPlayer = FindDirectionToPlayer();
        var directionToPlayerY = directionToPlayer.y;
        return directionToPlayerY;
    }

    public float GetEnemyAngleX()
    {
        Vector2 directionToPlayer = FindDirectionToPlayer();
        var directionToPlayerX = directionToPlayer.x;
        return directionToPlayerX;
    }

    public Quaternion GetEnemyRotation()
    {
        float offset = 90f;
        //var angleToPlayerDeg = FindAngleToPlayerDegree(FindDirectionToPlayer());

        Quaternion rotationDestination = Quaternion.AngleAxis(offset + angleToPlayerDeg, Vector3.forward);
        return Quaternion.Slerp(transform.rotation, rotationDestination, speedOfRotation * Time.deltaTime);
    }

    public float RotateToPlayer()
    {
        float offset = 90f;
        return offset * angleToPlayerDeg;
    }
}
