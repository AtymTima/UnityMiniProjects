using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    //cashed reference
    GameSession gameSession;
    MaxScoreKeeper maxScoreKeeper;
    TextMeshProUGUI scoreLabel;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        scoreLabel = GetComponent<TextMeshProUGUI>();
        maxScoreKeeper = FindObjectOfType<MaxScoreKeeper>();
    }

    void Update()
    {
        if (gameObject.tag == "MaxScore")
        {
            UpdateMaxScoreText();
        }
        else
        {
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreLabel.text = gameSession.GetScore().ToString();
    }

    private void UpdateMaxScoreText()
    {
        scoreLabel.text = maxScoreKeeper.GetMaxScore().ToString();
    }
}
