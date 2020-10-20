using UnityEngine;

[CreateAssetMenu(menuName = "My Falling Food/Yogurt")]
public class FallingYogurt : FallingFood
{
    private ObjectPool<GreekYogurt> yogurtPool;
    private GreekYogurt yogurtObject;

    public override void SpawnNextFood(Transform spawnPoint, Transform spawner)
    {
        yogurtPool = ObjectPool<GreekYogurt>.poolInstance;
        yogurtObject = yogurtPool.GetObject();
        yogurtObject.transform.localPosition = spawnPoint.localPosition;
        yogurtObject.transform.parent = spawner;
        yogurtObject.gameObject.SetActive(true);
    }
}
