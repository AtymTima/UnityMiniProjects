using UnityEngine;

public class GreekYogurt : MonoBehaviour
{
    private ObjectPool<GreekYogurt> currentPool;

    private void Start()
    {
        currentPool = ObjectPool<GreekYogurt>.poolInstance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            currentPool.ReturnToPool(this);
        }
    }
}
