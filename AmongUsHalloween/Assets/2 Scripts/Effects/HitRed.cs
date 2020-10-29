using UnityEngine;

public class HitRed : MonoBehaviour
{
    private ObjectPool<HitRed> hitRedPool;

    private void Start()
    {
        hitRedPool = ObjectPool<HitRed>.poolInstance;
    }

    public void EffectIsFinished()
    {
        gameObject.SetActive(false);
        hitRedPool.ReturnToPool(this);
    }
}
