using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    //cashed reference
    GameStatus gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalNumberOfScenes = SceneManager.sceneCountInBuildSettings - 2;

        if (currentSceneIndex <= totalNumberOfScenes)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            RestartTheGame();
        }
    }

    private void RestartTheGame()
    {
        gameStatus.ResetGame();
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartTheGame();
        }
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
