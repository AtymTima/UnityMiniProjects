using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //config params
    [Header("Enemy")]
    [SerializeField] public int health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.25f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float enemySizePadding = 0.8f;
    [Space]
    [SerializeField] int pointsPerEnemy = 100;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefabEnemy;
    [SerializeField] float projectileEnemySpeed = 3f;
    [SerializeField] float timeBetweenShotsEnemy = 0.5f;
    [Space]
    [SerializeField] AudioClip fireSoundEnemy;
    [SerializeField] float fireSoundVolume = 0.1f;

    [Header("Explosion")]
    [SerializeField] float durationVFX = 1f;
    [SerializeField] GameObject explosionVXF;
    [Space]
    [SerializeField] AudioClip explosionEnemySound;
    [SerializeField] float explosionSoundVolume = 0.05f;

    [SerializeField] GameObject player;

    //cashed reference
    Coroutine fireEnemyCoroutine;
    AudioSource audioSourceExplosionEnemy;
    SoundVFX soundVFX;
    GameSession gameSession;
   [SerializeField] GameObject gameObjectEnemy;
    EnemyRotation enemyRotation;
    FlashHit flashHit;

    // Start is called before the first frame update
    void Start()
    {
        RandomTimeBetweenShots();
        //audioSourceExplosionEnemy = GetComponent<AudioSource>();

        soundVFX = gameObjectEnemy.GetComponent<SoundVFX>();
        gameSession = FindObjectOfType<GameSession>();
        enemyRotation = FindObjectOfType<EnemyRotation>();
        player = GameObject.FindGameObjectWithTag("Player");

        flashHit = FindObjectOfType<FlashHit>();
    }

    void Update()
    {
        ShootAndCountDown();
    }

    private void ShootAndCountDown()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            fireEnemyCoroutine = StartCoroutine(EnemyFire());
            RandomTimeBetweenShots();
        }
    }

    IEnumerator EnemyFire()
    {
        var enemyPos = new Vector3(transform.position.x + enemySizePadding / 2,
         transform.position.y, transform.position.z);

        GameObject laserEnemy = Instantiate(laserPrefabEnemy, enemyPos, Quaternion.identity) as GameObject;

        if (gameObject.tag != "AngleEnemy")
        {
            laserEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileEnemySpeed);
        }
        else
        {
            float laserAngleX;
            var laserAngleY = enemyRotation.GetEnemyAngleY();
            laserAngleX = enemyRotation.GetEnemyAngleX();
            ProjectileWithAngle(laserEnemy, laserAngleX, laserAngleY);
        }

        soundVFX.PlaySound(fireSoundEnemy, fireSoundVolume);

        yield return new WaitForSeconds(timeBetweenShotsEnemy);
    }

    private void ProjectileWithAngle(GameObject laserEnemy, float laserAngleX, float laserAngleY)
    {
        try
        {
            //float laserAngleYTriangle = Mathf.Sqrt(projectileEnemySpeed * projectileEnemySpeed -
                                //laserAngleX * laserAngleX) * ifPlayerBelow;

            if (!float.IsNaN(laserAngleY))
            {
                laserEnemy.GetComponent<Rigidbody2D>().velocity =
                                                new Vector2(laserAngleX * projectileEnemySpeed,
                                                laserAngleY * projectileEnemySpeed);

                laserEnemy.transform.rotation = enemyRotation.GetEnemyRotation();
            }
            else
            {
                Destroy(laserEnemy);
                Debug.Log("Since Player is dead, enemy can't refer to target position for X and Y");
            }

        }
        catch (Exception)
        {
            Debug.Log("Laser Related Exception");
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {
        try
        {
            DamageDealer damageDealer = otherCollision.GetComponent<DamageDealer>();
            ReceiveHit(damageDealer, otherCollision);
            flashHit.FlashWhenHit(Time.time);
        }
        catch (Exception)
        {
            Debug.Log("Can't decrease the enemy health since the player is destroyed");
        }
    }

    private void RandomTimeBetweenShots()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    private void ReceiveHit(DamageDealer damageDealer, Collider2D otherCollision)
    {
        health -= damageDealer.GetDamage();
        damageDealer.HitLaser(otherCollision);


        if (health <= 0)
        {
            gameSession.AddToScore(pointsPerEnemy);

            player.GetComponent<Player>().AddPlayerHealth(pointsPerEnemy);
            ExplosionSoundVFX();
            SummonExplosion();
            damageDealer.DestroyObject(gameObject);
        }
    }

    private void SummonExplosion()
    {
        var enemyPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject explosionParticles = Instantiate(explosionVXF, enemyPos, Quaternion.identity) as GameObject;
        Destroy(explosionParticles, durationVFX);
    }

    private void ExplosionSoundVFX()
    {
        soundVFX.PlaySound(explosionEnemySound, explosionSoundVolume);
    }
}
