using UnityEngine;

public class FireShoot : MonoBehaviour
{
    private ObjectPool<FireShoot> fireShootPool;

    private void Start()
    {
        fireShootPool = ObjectPool<FireShoot>.poolInstance;
    }

    public void EffectIsFinished()
    {
        gameObject.SetActive(false);
        fireShootPool.ReturnToPool(this);
    }
}
