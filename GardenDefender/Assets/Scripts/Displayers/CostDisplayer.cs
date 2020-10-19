using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostDisplayer : MonoBehaviour
{
    [SerializeField] Cost cost;
    int currentCost;

    Text costText;

    private void Awake()
    {
        costText = GetComponent<Text>();
    }

    private void Start()
    {
        UpdateCostDisplayer(currentCost);
    }

    public void UpdateCostDisplayer(int currentCost)
    {
        this.currentCost = currentCost;
        costText.text = this.currentCost.ToString();
    }
}
