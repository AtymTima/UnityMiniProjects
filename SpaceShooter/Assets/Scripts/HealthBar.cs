using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //cashed reference
    Image healthBarImage;
    RawImage healthBarRawImage;
    Player player;
    Rect uvRect;

    float maxHealth;
    float healthPercentages;
    float speedOfMovingBar = 0.1f;

    private void Awake()
    {
        healthBarImage = transform.Find("Bar").GetComponent<Image>();
        healthBarRawImage = transform.Find("Bar").Find("Health Bar Filler").GetComponent<RawImage>();
        healthBarImage.fillAmount = 1f;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        maxHealth = player.GetPlayerHealth();
    }

    private void Update()
    {
        MovingBarAnimation();
    }

    private void MovingBarAnimation()
    {
        uvRect = healthBarRawImage.uvRect;
        uvRect.x -= speedOfMovingBar * Time.deltaTime;
        healthBarRawImage.uvRect = uvRect;
    }

    public void UpdateHealthBar(int playerHealth)
    {
        healthPercentages = playerHealth / maxHealth;
        healthBarImage.fillAmount = Mathf.Clamp(healthPercentages, 0f, maxHealth);
    }
}
