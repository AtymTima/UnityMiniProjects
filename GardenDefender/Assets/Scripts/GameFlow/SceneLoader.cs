using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentSceneIndex;

    [SerializeField] private GameObject timerObject;
    [SerializeField] private int timeBeforeNextLevel = 3;
    [SerializeField] GameController GameController;

    Timer timer;
    GameObject winningScreen;
    GameObject losingScreen;
    bool isLost;

    private void Awake()
    {
        timer = timerObject.GetComponent<Timer>();
        if (SceneManager.GetActiveScene().buildIndex < 2) { return; }
        winningScreen = GameObject.FindGameObjectWithTag("WinningScreen");
        losingScreen = GameObject.FindGameObjectWithTag("LoseScreen");

        winningScreen?.SetActive(false);
        losingScreen?.SetActive(false);
    }

    private void OnEnable()
    {
        BaseHealth.onBaseDestroyed += LoadGameOverScene;
        GameController.onLevelComplete += ShowWinningScrene;
        LevelLost.onLevelLost += RestartScene;
    }

    private void OnDisable()
    {
        BaseHealth.onBaseDestroyed -= LoadGameOverScene;
        GameController.onLevelComplete -= ShowWinningScrene;
        LevelLost.onLevelLost -= RestartScene;
    }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            MyAudioManager.instance.PlayAudio("LoadingSFX", false, 0, 0);
            StartCoroutine(timer.SetTimer(LoadNextScene, 2f));
        }
        else
        {
            MyAudioManager.instance.PlayAudio("MysticTown", false, 0, 0);
        }
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGameOverScene()
    {

        if (isLost) { return; }

        losingScreen?.SetActive(true);
        MyAudioManager.instance.PlayAudio("LevelLost", false, 0, 0);
        isLost = true;
    }

    private void ShowWinningScrene()
    {
        if (isLost) { return; }

        winningScreen?.SetActive(true);
        //MyAudioManager.instance.PlayAudio("LevelCompleted", false, 0, 0);
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        MyAudioManager.instance.PlayAudio("LevelCompleted", false, 0, 0);
        yield return new WaitForSeconds(timeBeforeNextLevel);
        GameController.ResetGameLevel();
        LoadNextScene();
    }

    private void RestartScene(bool restart)
    {
        Time.timeScale = 1;

        if (restart == true)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        else
        {
            SceneManager.LoadScene("2.Main Menu");
        }
    }
}


