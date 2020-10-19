using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Defender")]
public class Defender : ScriptableObject
{
    [SerializeField] GameObject defenderPrefab;
    [SerializeField] float defenderHealth = 250f;
    [SerializeField] int starCost = 1;

    public GameObject GetDefenderPrefab() { return defenderPrefab;  }
    public float GetDefenderHealth() { return defenderHealth; }
    public int GetDefenderStarCost() { return starCost; }
}
