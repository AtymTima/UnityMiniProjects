using UnityEngine;
using TMPro;

public class HitScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hitScoreLabel;
    private ObjectPool<HitScore> hitPool;
    private ScoreManager scoreManager;
    private bool autoSpawn;

    private void Start()
    {
        hitPool = ObjectPool<HitScore>.objectPool;
    }

    private void OnEnable()
    {
        scoreManager = transform.parent?.transform.parent?.GetComponent<DavidAnim>()?.scoreManager;
        if (scoreManager != null)
        {
            if (autoSpawn)
            {
                hitScoreLabel.text = scoreManager.playerScore.PointsPerSecond().ToString();
            }
            else
            {
                hitScoreLabel.text = scoreManager.playerScore.PointsPerClick().ToString();
            }
        }
        else
        {
            hitScoreLabel.text = 1.ToString();
        }
    }

    public void SetWhoSpawnedMe(bool autoSpawn)
    {
        this.autoSpawn = autoSpawn;
    }

    public void ReturnToPool()
    {
        hitPool.ReturnToPool(this);
    }
}
