using UnityEngine;

[CreateAssetMenu(menuName ="My Falling Food/Salad")]
public class FallingSalad : FallingFood
{
    private ObjectPool<Salad> saladPool;
    private Salad saladObject;

    public override void SpawnNextFood(Transform spawnPoint, Transform spawner)
    {
        saladPool = ObjectPool<Salad>.poolInstance;
        saladObject = saladPool.GetObject();
        saladObject.transform.localPosition = spawnPoint.localPosition;
        saladObject.transform.parent = spawner;
        saladObject.gameObject.SetActive(true);
    }
}
