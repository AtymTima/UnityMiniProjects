using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxScoreKeeper : MonoBehaviour
{
    public int currentMaxScore = 0;

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void saveMaxScore(int newMaxScore)
    {
        currentMaxScore = newMaxScore;
    }

    public int GetMaxScore()
    {
        return currentMaxScore;
    }
}
