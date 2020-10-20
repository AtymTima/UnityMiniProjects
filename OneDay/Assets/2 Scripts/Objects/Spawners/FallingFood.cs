using UnityEngine;

public abstract class FallingFood : ScriptableObject
{
    public abstract void SpawnNextFood(Transform spawnPoint, Transform spawner);
}
