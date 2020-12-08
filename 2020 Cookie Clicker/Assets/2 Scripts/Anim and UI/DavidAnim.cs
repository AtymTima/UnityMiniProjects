using UnityEngine;
using System;

public class DavidAnim : MonoBehaviour
{
    public static Action OnDavidDead = delegate { };
    [Header("Spawner")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Canvas spawnCanvas;
    private ObjectPool<HitScore> hitScorePool;
    private HitScore currentHitScore;
    private bool autoSpawn;

    [Header("Cashed References")]
    [SerializeField] private Animator davidAnim;
    [SerializeField] private SpriteRenderer davidSpRenderer;
    [SerializeField] public ScoreManager scoreManager;
    [SerializeField] private SoundManager soundManager;

    [Header("Parameters")]
    [SerializeField] private string isClicked = "isClicked";
    [SerializeField] private string isDead = "isDead";
    private bool hasCalled;
    private int frame;
    private Color32 currentColor;
    private Color32 originalColor = new Color32(85, 200, 85, 255);

    private void Awake()
    {
        PlayerInteractions.OnCookieClicked += OnClick;
        Spawner.OnAutoSpawn += AutoSpawn;
        davidSpRenderer.color = originalColor;
        currentColor = originalColor;
    }

    private void Start()
    {
        hitScorePool = ObjectPool<HitScore>.objectPool;
    }

    private void OnDestroy()
    {
        PlayerInteractions.OnCookieClicked -= OnClick;
        Spawner.OnAutoSpawn -= AutoSpawn;
    }

    private void AutoSpawn()
    {
        autoSpawn = true;
        davidAnim.SetTrigger(isClicked);
        SpawnHitScoreText();
    }

    private void OnClick()
    {
        autoSpawn = false;
        davidAnim.SetTrigger(isClicked);
        SpawnHitScoreText();
    }

    private void SpawnHitScoreText()
    {
        currentHitScore = hitScorePool.GetObject();
        currentHitScore.transform.SetParent(spawnCanvas.transform);
        currentHitScore.transform.localPosition = spawnPoint.localPosition;
        currentHitScore.transform.localScale = spawnPoint.localScale;
        currentHitScore.GetComponent<HitScore>()?.SetWhoSpawnedMe(autoSpawn);
        currentHitScore.gameObject.SetActive(true);
        AdjustColors();
    }

    private void AdjustColors()
    {
        frame++;
        if (frame % 1 == 0)
        {
            if (scoreManager.currentScore <= 10000)
            {
                currentColor.r = (byte)(85 + 115 * scoreManager.currentScore / 10000);
                davidSpRenderer.color = currentColor;
            }
            else if (scoreManager.currentScore <= 100000)
            {
                currentColor.r = 200;
                currentColor.g = (byte)(200 - 115 * scoreManager.currentScore / 100000);
                davidSpRenderer.color = currentColor;
            }
            else if (scoreManager.currentScore < 1000000)
            {
                currentColor.r = 200;
                currentColor.g = 85;
                currentColor.g = (byte)(85 + 115 * scoreManager.currentScore / 1000000);
                davidSpRenderer.color = currentColor;
            }
            else if (scoreManager.currentScore > 100000)
            {
                if (!hasCalled)
                {
                    davidAnim.SetTrigger(isDead);
                    soundManager.PlayExplosionSFX();
                    hasCalled = true;
                }
            }
        }
    }

    public void OnDead()
    {
        OnDavidDead();
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
