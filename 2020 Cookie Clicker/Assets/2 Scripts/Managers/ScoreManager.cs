using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private MenuVaccine[] menuVaccine;
    [SerializeField] private ImmuneManager immune;
    [SerializeField] private SoundManager soundManager;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private CanvasGroup[] canvasGroup;
    [SerializeField] private GameObject[] shopMenus;
    [SerializeField] private TextMeshProUGUI congratsText;
    public PlayerScore playerScore;
    public int currentScore = 0;
    private int currentIndex;

    private void Awake()
    {
        PlayerInteractions.OnCookieClicked += OnClick;
        Spawner.OnAutoSpawn += ChangeAutoScore;
        DavidAnim.OnDavidDead += OnEnd;
        scoreLabel.SetText(0.ToString());
        playerScore = new PlayerScore();
        for (int i = currentIndex; i < menuVaccine.Length; i++)
        {
            ChangeBtnColors(i, false, 0.5f);
        }
    }

    private void OnDestroy()
    {
        PlayerInteractions.OnCookieClicked -= OnClick;
        Spawner.OnAutoSpawn -= ChangeAutoScore;
        DavidAnim.OnDavidDead -= OnEnd;
    }

    private void OnClick()
    {
        currentScore += playerScore.PointsPerClick();
        CheckBtnStates();
    }

    public void OnBtnPressed(int i)
    {
        soundManager.PlayPurchaseSFX();
        playerScore.SetPointsPerClick(menuVaccine[i].perClick);
        currentScore -= menuVaccine[i].cost;
        CheckBtnStates();
    }

    public void CheckBtnStates()
    {
        scoreLabel.SetText(currentScore.ToString());
        for (int i = menuVaccine.Length-1; i >= currentIndex; i--)
        {
            if (menuVaccine[i].cost <= currentScore)
            {
                ChangeBtnColors(i, true, 1);
                if (currentIndex == 5) { break; }
                currentIndex++;
            }
            else
            {
                ChangeBtnColors(i, false, 0.5f);
                if (currentIndex >= i && currentIndex != 0)
                {
                    currentIndex--;
                    currentIndex = Mathf.Clamp(currentIndex, 0, 5);
                }
            }
        }
    }

    private void ChangeBtnColors(int i, bool state, float alpha)
    {
        menuVaccine[i].menuBtn.enabled = state;
        canvasGroup[i].alpha = alpha;
        immune.menuImmunity[i].menuBtn.enabled = state;
        immune.canvasGroup[i].alpha = alpha;
    }

    private void ChangeAutoScore()
    {
        currentScore += playerScore.PointsPerSecond();
        scoreLabel.SetText(currentScore.ToString());
        CheckBtnStates();
    }

    private void OnEnd()
    {
        for (int i=0; i < shopMenus.Length; i++)
        {
            shopMenus[i].SetActive(false);
        }
        congratsText.gameObject.SetActive(true);
    }
}
