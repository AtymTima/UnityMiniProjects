using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreoSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] OreoDisplayer oreoDisplayer;
    [SerializeField] ViagogoMovement viagogoMovement;

    private int randomIndex;
    private int previousIndex;
    private Vector2 lastSpawnPoint;
    private ObjectPool<Oreo> oreoPool;
    private Oreo currentOreo;

    private void Awake()
    {
        Oreo.OnOreoCollected += SpawnNextOreo;
    }

    private void Start()
    {
        oreoPool = ObjectPool<Oreo>.poolInstance;
        SpawnNextOreo();

    }

    private void OnDestroy()
    {
        Oreo.OnOreoCollected -= SpawnNextOreo;
    }

    public void SpawnNextOreo()
    {
        if (oreoDisplayer.CurrentScore == 3)
        {
            viagogoMovement.ReverseDirection();
            return;
        }

        currentOreo = oreoPool.GetObject();
        do
        {
            randomIndex = Random.Range(0, spawnPoints.Length - 1);
        }
        while (previousIndex == randomIndex);
        currentOreo.transform.position = spawnPoints[randomIndex].localPosition;
        currentOreo.transform.parent = spawnPoints[randomIndex];
        currentOreo.gameObject.SetActive(true);
        previousIndex = randomIndex;
    }
}
