using UnityEngine;

public class Salad : MonoBehaviour
{
    private ObjectPool<Salad> currentPool;

    private void Start()
    {
        currentPool = ObjectPool<Salad>.poolInstance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            currentPool.ReturnToPool(this);
            switch(collision.CompareTag("Player"))
            {
                case true:
                    collision.gameObject.GetComponent<SoundPlayer>().EatSound();
                    break;
            }
        }
    }
}
