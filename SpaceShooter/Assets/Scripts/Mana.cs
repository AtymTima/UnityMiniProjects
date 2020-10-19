using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana
{
    static float currentMana = 75f;
    float manaUsagePerFrame = 25f;

    float maxMana = 100f;
    float restoreManaSpeed = 40f;

    //cashed reference

    public void ReduceMana()
    {
        currentMana -= manaUsagePerFrame * Time.deltaTime;
        currentMana = Mathf.Clamp(currentMana, 0f, maxMana);
    }

    public void RestoreMana()
    {
        currentMana += restoreManaSpeed * Time.deltaTime;
        currentMana = Mathf.Clamp(currentMana, 0f, maxMana);
    }

    public float GetManaNormalized()
    {
        return currentMana / maxMana;
    }
}
