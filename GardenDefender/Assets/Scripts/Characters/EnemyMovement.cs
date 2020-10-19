using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float speedMovement = 2f;

    public void GetEnemySpeed(float speed)
    {
        speedMovement = speed;
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * speedMovement * Vector2.left);
    }
}
