using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //config params
    [SerializeField] float timeBeforeGameOver = 2f;
    [SerializeField] AudioClip palpatineLaugh;

    //cashed reference
    GameSession gameSession;
    SoundVFX soundVFX;

    private void Start()
    {
        soundVFX = FindObjectOfType<SoundVFX>();
        gameSession = FindObjectOfType<GameSession>();
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalNumberOfScenes = SceneManager.sceneCountInBuildSettings - 1;

        if (currentSceneIndex < totalNumberOfScenes)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            ResetScoreAndGame(0);
        }
    }

    public void LoadFirstLevel()
    {
        ResetScoreAndGame(1);
    }

    private void ResetScoreAndGame(int sceneIndex)
    {
        gameSession = FindObjectOfType<GameSession>();
        gameSession.ResetTheGame();
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(DelayOncePlayerDies());
        gameSession.PlayLoseSound();
    }

    IEnumerator DelayOncePlayerDies()
    {
        yield return new WaitForSeconds(timeBeforeGameOver);
        LoadNextScene();
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
