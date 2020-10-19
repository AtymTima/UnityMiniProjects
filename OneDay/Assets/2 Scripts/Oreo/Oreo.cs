using UnityEngine;
using System;

public class Oreo : MonoBehaviour
{
    public static Action OnOreoCollected;
    [SerializeField] private bool isRespawn;
    [SerializeField] private Animator oreoAnim;
    private string playerTag = "Player";
    private string isReturned = "isReturned";

    private ObjectPool<Oreo> oreoPool;

    private void Start()
    {
        oreoPool = ObjectPool<Oreo>.poolInstance;
    }

    public void OnDissapear()
    {
        switch (isRespawn)
        {
            case true:
                oreoAnim.SetTrigger(isReturned);
                oreoPool.ReturnToPool(this);
                OnOreoCollected?.Invoke();
                break;
        }
    }
}
