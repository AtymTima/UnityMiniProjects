using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    //config params
    int totalNumberOfBlocks;

    //cashed references
    SceneLoader sceneLoader;
    GameStatus gameStatus;

    void Start()
    {
        totalNumberOfBlocks = GameObject.FindGameObjectsWithTag("Breakable").Length;
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    public void CountBrokenBlocks()
    {
        totalNumberOfBlocks--;
        CountHowManyBlocksRemain();
    }

    public void CountHowManyBlocksRemain()
    {
        if (totalNumberOfBlocks == 0)
        {
            LoadNextLevel();
            gameStatus.CountHowMuchPointsPerBlock();
        }
    }

    public void LoadNextLevel()
    {
        sceneLoader.LoadNextScene();
    }
}
