using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T: Component
{
    public static ObjectPool<T> poolInstance { get; private set; }
    [SerializeField] private T prefabObject;
    private Queue<T> poolQueue = new Queue<T>();
    private T currentObject;

    private void Awake()
    {
        poolInstance = this;
    }

    public T GetObject()
    {
        switch (poolQueue.Count)
        {
            case 0:
                AddToPool(1);
                break;
        }
        currentObject = poolQueue.Dequeue();
        return currentObject;
    }

    public void AddToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            currentObject = Instantiate(prefabObject);
            currentObject.gameObject.SetActive(false);
            poolQueue.Enqueue(currentObject);
        }
    }

    public void ReturnToPool(T component)
    {
        component.gameObject.SetActive(false);
        poolQueue.Enqueue(component);
    }
}
