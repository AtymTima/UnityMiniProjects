using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseDisplayer : MonoBehaviour
{
    //config params
    [SerializeField] BaseHealth baseHealth;

    //cashed reference
    Text baseDisplayer;

    private void Awake()
    {
        baseDisplayer = GetComponent<Text>();
    }

    public void UpdateHealthDisplayer(float baseHealth)
    {
        baseDisplayer.text = baseHealth.ToString();
    }
}
