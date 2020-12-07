using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T:Component
{
    public static ObjectPool<T> objectPool { get; private set; }
    [SerializeField] private T prefabObject;
    private Queue<T> poolQueue = new Queue<T>();
    private T currentObject;

    private void Awake()
    {
        objectPool = this;
    }

    public T GetObject()
    {
        if (poolQueue.Count == 0)
        {
            AddObject();
        }
        currentObject = poolQueue.Dequeue();
        return currentObject;
    }

    public void AddObject()
    {
        currentObject = Instantiate(prefabObject) as T;
        currentObject.gameObject.SetActive(false);
        poolQueue.Enqueue(currentObject);
    }

    public void ReturnToPool(T returnedObject)
    {
        returnedObject.gameObject.SetActive(false);
        poolQueue.Enqueue(returnedObject);
    }
}
