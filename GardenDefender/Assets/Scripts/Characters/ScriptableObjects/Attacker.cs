using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Attacker")]
public class Attacker : ScriptableObject
{
    [Range(0f, 5f)]
    [SerializeField] float speedMove = 2f;
    [SerializeField] GameObject enemyPrejab;
    [SerializeField] float enemyHealth = 100f;

    public float GetSpeedMove() { return speedMove; }

    public float GetEnemyHealth() { return enemyHealth; }

    public GameObject GetEnemyPrefab() { return enemyPrejab; }
}
