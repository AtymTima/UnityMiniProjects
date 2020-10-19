using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplayer : MonoBehaviour
{
    //config params
    int currentHealth;

    //cashed reference
    TextMeshProUGUI healthIndicator;
    GameSession gameSession;

    void Start()
    {
        healthIndicator = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        UpdateHealthIndicator();
    }

    public void UpdateHealthIndicator()
    {
        currentHealth = gameSession.GetHealth();
        if (currentHealth > 0)
        {
            healthIndicator.text = currentHealth.ToString();
        }
        else
        {
            healthIndicator.text = "0";
        }
    }

    private void Update()
    {
    }

}
