using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cost : MonoBehaviour
{
    [SerializeField] CostDisplayer costDisplayer;
    [SerializeField] List<Defender> defenders;
    int currentCostIndex;
    Defender defender;

    private void OnEnable()
    {
        DefenderChooseBtn.onPlayerSpawned += DetermineIndex;
    }

    private void OnDisable()
    {
        DefenderChooseBtn.onPlayerSpawned -= DetermineIndex;
    }

    private void DetermineIndex(int currentIndex)
    {
        currentCostIndex = defenders[currentIndex].GetDefenderStarCost();
        costDisplayer.UpdateCostDisplayer(currentCostIndex);
    }
}
