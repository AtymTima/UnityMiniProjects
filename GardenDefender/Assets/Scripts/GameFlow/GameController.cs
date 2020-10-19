using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Controller")]
public class GameController : ScriptableObject
{
    [SerializeField] public int enemyCount;
    [SerializeField] public int enemyDestroyed;
    [SerializeField] public float maxTimer = 25f;
    [SerializeField] public bool isTimerStopped;
    [SerializeField] public int numberOfSpawners;

    private float maxTimerStart;
    public int numberOfSpawnersStart;

    public delegate void OnLevelComplete();
    public static event OnLevelComplete onLevelComplete = delegate { };

    private void OnDisable()
    {
        maxTimerStart = maxTimer;
        numberOfSpawnersStart = 6;
    }

    private void OnEnable()
    {
        ResetGameLevel();
    }

    public void ResetGameLevel()
    {
        enemyCount = 0;
        maxTimer = maxTimerStart;
        isTimerStopped = false;
        enemyDestroyed = 0;
        numberOfSpawners = numberOfSpawnersStart;
    }

    public void IsLevelComplete()
    {
        if (isTimerStopped && enemyDestroyed >= enemyCount)
        {
            onLevelComplete?.Invoke();
        }
    }
}
