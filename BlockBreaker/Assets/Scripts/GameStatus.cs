using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    //config params
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed=1f;
    int pointsPerBlockDestroyed = 12;
    [SerializeField] int currentScore = 0;
    //[SerializeField] bool isAutoPlayEnabled=false;
    int currentScene;

    //cashed references
    [SerializeField] TextMeshProUGUI scoreText;
    public static GameStatus instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = gameSpeed;
        UpdateScoreText();
        CountHowMuchPointsPerBlock();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"{currentScore}";
    }

    public void CountHowMuchPointsPerBlock()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        pointsPerBlockDestroyed *= (currentScene + 1);
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        UpdateScoreText();
    }

    //public bool IsAutoPlayEnabled()
    //{
    //    return isAutoPlayEnabled;
    //}
}
