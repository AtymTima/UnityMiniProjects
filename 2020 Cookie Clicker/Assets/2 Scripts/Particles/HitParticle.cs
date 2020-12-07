using UnityEngine;

public class HitParticle : MonoBehaviour
{
    private ObjectPool<HitParticle> hitPool;
    [SerializeField] private ParticleSystem hitSystem;
    private float startTime;
    private float totalDuration;

    private void OnEnable()
    {
        startTime = Time.time;
        totalDuration = hitSystem.main.duration + hitSystem.main.startLifetimeMultiplier;
    }

    private void Start()
    {
        hitPool = ObjectPool<HitParticle>.objectPool;
    }

    private void Update()
    {
        if (Time.time - startTime > totalDuration)
        {
            hitPool.ReturnToPool(this);
        }
    }
}
