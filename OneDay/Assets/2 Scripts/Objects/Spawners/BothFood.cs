using UnityEngine;

[CreateAssetMenu(menuName = "My Falling Food/Both")]
public class BothFood : FallingFood
{
    private ObjectPool<GreekYogurt> yogurtPool;
    private ObjectPool<Salad> saladPool;
    private Component currentComponent;

    public override void SpawnNextFood(Transform spawnPoint, Transform spawner)
    {
        if (Random.value > 0.5)
        {
            yogurtPool = ObjectPool<GreekYogurt>.poolInstance;
            currentComponent = yogurtPool.GetObject();

        }
        else
        {
            saladPool = ObjectPool<Salad>.poolInstance;
            currentComponent = saladPool.GetObject();
        }
        currentComponent.transform.localPosition = spawnPoint.localPosition;
        currentComponent.transform.parent = spawner;
        currentComponent.gameObject.SetActive(true);
    }
}
