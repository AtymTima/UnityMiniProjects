using UnityEngine;
using TMPro;
using System;

public class GameSession : MonoBehaviour
{

    //config params
    [Header("Player Params")]
    [SerializeField] int currentScore;
    [SerializeField] GameObject player;
    [SerializeField] bool isPlayerDead;
    int playerHealth = 2500;

    [Header("Boss")]
    [SerializeField] bool isEnemySpawned;
    [SerializeField] bool isEnemyDead;
    [Space]

    [SerializeField] AudioClip losePlayer;
    [SerializeField] AudioClip loseBoss;
    [SerializeField] float loseVolume = 0.5f;
    [Space]

    [SerializeField] GameObject healthBoss; //Spawned Boss Object 
    [SerializeField] GameObject healthIndicatorText; //Health Indicator TMPro
    Transform transformBoss;

    // Game Units Screen 
    float gameWidth;
    float convertToGameUnits;
    int healthPoints;

    bool isLastScene;

    //cashed reference
    MaxScoreKeeper maxScoreKeeper;
    DamageDealer damageDealer;
    EnemySpawner enemySpawner;
    SoundVFX soundVFX;


    void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        soundVFX = FindObjectOfType<SoundVFX>();

        DetermineScreenSize();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Player>().GetPlayerHealth();
    }

    private void Update()
    {
        if (isEnemySpawned && !isEnemyDead)
        {
            if (healthBoss != null)
            {
                ControlBoss();
            }
            else if (!isPlayerDead && healthBoss == null)
            {
                DestroyBossWhenFinished();
            }
        }

        if (isLastScene)
        {
            StartPostDeathScene();
        }
    }

    private void ControlBoss()
    {
        try
        {
            UpdateHealthBar();
            MoveHealthBarBoss();
        }
        catch (Exception)
        {
            Debug.Log("Health Bar Of The Enemy is NULL");
        }
    }

    private void DestroyBossWhenFinished()
    {
        Destroy(healthIndicatorText);
        soundVFX.PlaySound(loseBoss, loseVolume);
        isEnemyDead = true;
    }

    private void StartPostDeathScene()
    {
        soundVFX.PlaySound(losePlayer, loseVolume);
        isPlayerDead = true;
        isLastScene = false;
    }

    public void PlayLoseSound()
    {
        isLastScene = true;
    }

    private void MoveHealthBarBoss()
    {
        var p1 = transformBoss.TransformPoint(0, 0, 0);
        var p2 = transformBoss.TransformPoint(1, 1, 0);
        var w = (p2.x - p1.x) - 0.25f;
        var h = (p2.y - p1.y) * 2;
        healthIndicatorText.transform.position = new Vector2(transformBoss.position.x + w, transformBoss.position.y + h) / convertToGameUnits;
    }

    private void UpdateHealthBar()
    {
        healthPoints = healthBoss.GetComponent<Enemy>().health;
        if (healthPoints > 0)
        {
            healthIndicatorText.GetComponent<TextMeshProUGUI>().text = healthPoints.ToString();
        }
    }

    public void GetBossPosition(GameObject bossObject)
    {
        transformBoss = bossObject.transform;
        healthBoss = bossObject;
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int pointsForEnemy)
    {
        currentScore += pointsForEnemy;
        CheckScore();
    }

    void CheckScore()
    {
        if (currentScore > 3000 && !isEnemySpawned)
        {
            enemySpawner.SpawnBoss();
            isEnemySpawned = true;
        }
    }

    public void CheckAndUpdateMaxScore()
    {
        maxScoreKeeper = FindObjectOfType<MaxScoreKeeper>();
        if (currentScore > maxScoreKeeper.GetMaxScore())
        {
            maxScoreKeeper.saveMaxScore(currentScore);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    private void DetermineScreenSize()
    {
        float height = Camera.main.orthographicSize * 2.0f;
        gameWidth = height * Screen.width / Screen.height;
        convertToGameUnits = gameWidth / Screen.width;
    }

    public bool IsCloseToLeftOrRight()
    {
        var averagePos = healthBoss.transform.position.x * convertToGameUnits;
        if (averagePos <= 0.1)
        {
            return true;
        }
        return false;
    }

    public int GetHealth()
    {
        return player.GetComponent<Player>().GetPlayerHealth();
    }

    public void ResetTheGame()
    {
        Destroy(gameObject);
    }

}
