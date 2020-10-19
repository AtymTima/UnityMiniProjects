using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    //configuration parameters
    [Header("Player")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float playerSizePadding = 0.8f;
    [SerializeField] int healthPlayer = 500;
    [Space]
    [SerializeField] AudioClip palpatineLaugh;
    [SerializeField] float laughPalpatineVolume = 0.2f;


    [Header("Explosion")]
    [SerializeField] float durationVFX = 1f;
    [SerializeField] GameObject explosionVXF;
    [SerializeField] AudioClip explosionPlayerSFX;
    [SerializeField] float explosionPlayerVolume = 0.1f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserProjectileSpeed = 10f;
    [SerializeField] float projectileTimeFire = 0.1f;

    [Space]

    [SerializeField] AudioClip fireSoundPlayer;
    [SerializeField] float fireVolume = 0.05f;

    [Header("Power Ups")]
    [SerializeField] AudioClip healthPointsAudio;
    [SerializeField] float healthPointsVolume = 0.75f;
    [Space]
    [SerializeField] AudioClip superTurrelAudio;
    [SerializeField] float superTurrelVolume = 0.5f;

    [Header("Mana")]
    [SerializeField] float rechargePeriod = 0.5f;
    [SerializeField] AudioClip needRechargeSFX;
    [SerializeField] AudioClip readyToFireResponseSFX;
    [SerializeField] float responsesVolume = 0.15f;

    [Header("Camera Shake")]
    [SerializeField] float shakeDuration = 0.15f;
    [SerializeField] float shakeMagnitude = 0.15f;

    bool isFireBlocked;
    float charge;

    float worldMinX;
    float worldMaxX;
    float worldMinY;
    float worldMaxY;

    //cashed references
    Coroutine fireCoroutine;
    SoundVFX soundVFX;
    FlashHit flashHit;
    CameraShake cameraShake;

    SceneLoader sceneLoader;
    GameSession gameSession;

    HealthDisplayer healthDisplayer;
    HealthBar healthBar;
    ManaBar manaBar;
    Mana mana;

    private void Awake()
    {
        mana = new Mana();
        flashHit = FindObjectOfType<FlashHit>();
    }

    void Start()
    {
        SetUpMoveBoundaries();
        soundVFX = GetComponent<SoundVFX>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameSession = FindObjectOfType<GameSession>();
        healthDisplayer = FindObjectOfType<HealthDisplayer>();
        healthBar = FindObjectOfType<HealthBar>();
        manaBar = FindObjectOfType<ManaBar>();
        cameraShake = FindObjectOfType<CameraShake>();
    }

    void Update()
    {
        MovePlayer();
        PlayerFire();
        RechargeIfZero();
    }

    private void PlayerFire()
    {
        if (Input.GetButtonDown("Fire1") && (isFireBlocked == false))
        {
            manaBar.IsManaUsed(true);
            fireCoroutine = StartCoroutine(FireCountinuously());
        }
        if (Input.GetButtonUp("Fire1") || (isFireBlocked == true))
        {
            StopCoroutine(fireCoroutine);
            manaBar.IsManaUsed(false);
        }
    }

    IEnumerator FireCountinuously()
    {
        while (true)
        {
            var playerPos = new Vector3(transform.position.x
                            + playerSizePadding / 2,
                            transform.position.y + 0.2f, transform.position.z);
            GameObject laser = Instantiate(laserPrefab, playerPos, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserProjectileSpeed);
            soundVFX.PlaySound(fireSoundPlayer, fireVolume);

            yield return new WaitForSeconds(projectileTimeFire);
        }
    }

    private void RechargeIfZero()
    {
        charge = mana.GetManaNormalized();
        if (charge <= 0f)
        {
            StartCoroutine(BlockFireForPeriod());
        }
    }

    IEnumerator BlockFireForPeriod()
    {
        isFireBlocked = true;
        soundVFX.PlaySound(needRechargeSFX, responsesVolume);
        yield return new WaitForSeconds(rechargePeriod);
        isFireBlocked = false;
        soundVFX.PlaySound(readyToFireResponseSFX, responsesVolume);
    }

    private void MovePlayer()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, worldMinX, worldMaxX);
        var nexYpos = Mathf.Clamp(transform.position.y + deltaY, worldMinY, worldMaxY);
        transform.position = new Vector2(newXPos, nexYpos);
    }

    public Transform GetPlayerPosition()
    {
        return transform;
    }

    public void AddPlayerHealth(int newHealthPerObject)
    {
        healthPlayer += newHealthPerObject / 10;
        healthDisplayer.UpdateHealthIndicator();
    }

    public void DropHealth(DamageDealer damageDealer)
    {
        healthPlayer -= damageDealer.GetDamage();
        healthDisplayer.UpdateHealthIndicator();
    }

    public void DropHealthToZero()
    {
        healthPlayer = 0;
        healthDisplayer.UpdateHealthIndicator();
    }

    public int GetPlayerHealth()
    {
        return healthPlayer;
    }

    public void GetTurrelUpSettings(float time, float speedFactor)
    {
        StartCoroutine(SpeedUpFireForSeconds(time, speedFactor));
    }

    private IEnumerator SpeedUpFireForSeconds(float time, float speedFactor)
    {
        float currentLaserSpeed = laserProjectileSpeed;
        float currentInstantiateTime = projectileTimeFire;

        laserProjectileSpeed *= speedFactor;
        projectileTimeFire /= speedFactor;

        yield return new WaitForSeconds(time);

        laserProjectileSpeed = currentLaserSpeed;
        projectileTimeFire = currentInstantiateTime;
    }

    private void OnTriggerEnter2D(Collider2D laserCollision)
    {
        if (laserCollision.tag == "PowerUp")
        {
            flashHit.FlashWhenHit(Time.time);
            healthBar.UpdateHealthBar(healthPlayer);
            soundVFX.PlaySound(healthPointsAudio, healthPointsVolume);
            return;
        }

            DamageDealer damageDealer = laserCollision.GetComponent<DamageDealer>();
            ReceiveHit(damageDealer, laserCollision);
            flashHit.FlashWhenHit(Time.time);
            shakeCameraWhenHit();

        if (laserCollision.tag == "Boss" || laserCollision.tag == "bombProjectile")
        {
            DropHealthToZero();
            DiePlayer();
            LoadNextSceneAfterDeath();
        }
    }

    private void ReceiveHit(DamageDealer damageDealer, Collider2D laserCollision)
    {
        DropHealth(damageDealer);
        healthBar.UpdateHealthBar(healthPlayer);

        damageDealer.HitLaser(laserCollision);

        if (healthPlayer <= 0)
        {
            healthBar.UpdateHealthBar(0);
            DiePlayer();
            LoadNextSceneAfterDeath();
        }
    }

    private void DiePlayer()
    {
        manaBar.IsManaUsed(false);
        gameSession.CheckAndUpdateMaxScore();
        SummonExplosion();
        if (healthPlayer <= 0)
        {
            soundVFX.PlaySound(explosionPlayerSFX, explosionPlayerVolume);
        }
    }

    private void LoadNextSceneAfterDeath()
    {
        sceneLoader.LoadGameOverScene();
        Destroy(gameObject);
    }

    private void shakeCameraWhenHit()
    {
        StartCoroutine(cameraShake.ShakeCamera(shakeDuration, shakeMagnitude));
    }

    private void SummonExplosion()
    {
        var playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject explosionParticles = Instantiate(explosionVXF, playerPos, Quaternion.identity) as GameObject;
        Destroy(explosionParticles, durationVFX);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        worldMinX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        worldMaxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - playerSizePadding;

        worldMinY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        worldMaxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - playerSizePadding;
    }
}
