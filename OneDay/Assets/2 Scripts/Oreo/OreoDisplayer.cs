using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class OreoDisplayer : MonoBehaviour
{
    [SerializeField] private SoundPlayer soundPlayer;
    [SerializeField] private OreoSpawner oreoSpawner;

    [SerializeField] private Image textDisplayer;
    [SerializeField] private TextMeshProUGUI scoreText;
    public int CurrentScore { get; set; }
    StringBuilder currentText = new StringBuilder("0");

    private void Awake()
    {
        ResetScore();
    }

    public void UpdateScore()
    {
        CurrentScore++;
        ChangeText();
        textDisplayer.enabled = true;
    }

    public void RenewScoreAfterHit()
    {
        if (oreoSpawner != null)
        {
            ResetScore();
            oreoSpawner.SpawnNextOreo();
        }
    }

    public void SubstractScore()
    {
        CurrentScore--;
        CurrentScore = Mathf.Clamp(CurrentScore, 0, 5);
        ChangeText();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        ChangeText();
        textDisplayer.enabled = false;
    }

    private void ChangeText()
    {
        currentText.Clear();
        currentText.Append(CurrentScore);
        scoreText.SetText(currentText);
    }
}
