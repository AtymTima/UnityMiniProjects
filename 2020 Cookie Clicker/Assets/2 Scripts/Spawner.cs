using UnityEngine;
using System.Collections;
using System;

public class Spawner : MonoBehaviour
{
    public static Action OnAutoSpawn = delegate { };
    private ObjectPool<HitParticle> hitPool;
    [SerializeField] private Transform spawnerTransform;
    [SerializeField] private ScoreManager scoreManager;
    private HitParticle hitParticle;
    private Coroutine currentCoroutine;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(1.5f);

    private void Awake()
    {
        PlayerInteractions.OnCookieClicked += OnClick;
        ImmuneManager.StartAutoSpawn += StartCoroutine;
    }

    private void Start()
    {
        hitPool = ObjectPool<HitParticle>.objectPool;
    }

    private void OnDestroy()
    {
        PlayerInteractions.OnCookieClicked -= OnClick;
        ImmuneManager.StartAutoSpawn -= StartCoroutine;
    }

    private void OnClick()
    {
        SpawnParticle();
        hitParticle.gameObject.SetActive(true);
    }

    private void SpawnParticle()
    {
        hitParticle = hitPool.GetObject();
        hitParticle.transform.localPosition = spawnerTransform.localPosition;
        hitParticle.transform.parent = spawnerTransform;
    }

    private void StartCoroutine()
    {
        currentCoroutine = StartCoroutine(OnAutoSpawnLoop());
    }

    private IEnumerator OnAutoSpawnLoop()
    {
        while(true)
        {
            OnClick();
            OnAutoSpawn();
            yield return waitForSeconds;
        }
    }
}
