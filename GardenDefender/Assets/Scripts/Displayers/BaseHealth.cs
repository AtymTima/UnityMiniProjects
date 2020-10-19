using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] BaseDisplayer baseDisplayer;
    [SerializeField] float currentBaseHealth = 500f;

    public delegate void OnBaseDestroyed();
    public static event OnBaseDestroyed onBaseDestroyed = delegate { };

    private void OnEnable()
    {
        Lose.onBaseTriggered += UpdateBaseHealth;
    }

    private void OnDisable()
    {
        Lose.onBaseTriggered -= UpdateBaseHealth;
    }

    private void Start()
    {
        UpdateBaseHealth(0);
    }

    private void UpdateBaseHealth(float damageToBase)
    {
        currentBaseHealth -= damageToBase;
        baseDisplayer.UpdateHealthDisplayer(currentBaseHealth);
        CheckIfHealthRemains();
    }

    private void CheckIfHealthRemains()
    {
        if (currentBaseHealth <= 0)
        {
            onBaseDestroyed?.Invoke();
        }
    }
}
