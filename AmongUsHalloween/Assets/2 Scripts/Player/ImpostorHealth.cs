using UnityEngine;

public class ImpostorHealth : MonoBehaviour
{
    [Header("Cashed Reference")]
    [SerializeField] private PlayerDeath playerDeath;
    [SerializeField] private PlayerSounds playerSounds;
    [SerializeField] private Rigidbody2D impostorRB;
    [Header("Parameters")]
    [SerializeField] private int initialHealth = 200;
    [SerializeField] private int forceMultiplier = 8;
    public int RemainingHealth { get; set; }

    private void Awake()
    {
        RemainingHealth = initialHealth;
    }

    public void DecreaseHealth(int damage, float damageFromAngle)
    {
        RemainingHealth -= damage;
        float xcomponent = Mathf.Cos(damageFromAngle * Mathf.PI / 180) * damage * forceMultiplier;
        float ycomponent = Mathf.Sin(damageFromAngle * Mathf.PI / 180) * damage * forceMultiplier;
        Vector3 forceApplied = new Vector3(ycomponent, 0, xcomponent);
        impostorRB.AddForce(forceApplied);
        if (RemainingHealth <= 0)
        {
            playerDeath.IsKilled();
            playerSounds.PlayHitSound();
        }
    }
}
