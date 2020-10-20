using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnerTransform;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private FallingFood[] fallingFoods;
    private FallingFood currentFood;
    private ObjectPool<Component> currentPool;
    private Component currentObject;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);

    private void Awake()
    {
        currentFood = fallingFoods[0];
    }

    private IEnumerator Start()
    {
        while(true)
        {
            yield return waitForSeconds;
            currentFood.SpawnNextFood(spawnPoints[Random.Range(0, spawnPoints.Length)], spawnerTransform);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            currentFood = fallingFoods[0];
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            currentFood = fallingFoods[1];
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            currentFood = fallingFoods[2];
        }
    }
}
