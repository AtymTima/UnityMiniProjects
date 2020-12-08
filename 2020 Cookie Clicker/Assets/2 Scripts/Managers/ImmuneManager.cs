using UnityEngine;
using System;

public class ImmuneManager : MonoBehaviour
{
    public static Action StartAutoSpawn = delegate { };
    [SerializeField] public MenuImmunity[] menuImmunity;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] public CanvasGroup[] canvasGroup;
    private int currentIndex;
    private bool pressedFirstTime;

    private void Awake()
    {
        for (int i = currentIndex; i < menuImmunity.Length; i++)
        {
            ChangeBtnColors(i, false, 0.5f);
        }
    }

    public void OnBtnPressed(int i)
    {
        soundManager.PlayPurchaseSFX();
        scoreManager.playerScore.SetPointsPerSecond(menuImmunity[i].perTwoSeconds);
        scoreManager.currentScore -= menuImmunity[i].cost;
        scoreManager.CheckBtnStates();
        if(!pressedFirstTime)
        {
            StartAutoSpawn();
        }
        pressedFirstTime = true;
    }

    private void ChangeBtnColors(int i, bool state, float alpha)
    {
        menuImmunity[i].menuBtn.enabled = state;
        canvasGroup[i].alpha = alpha;
    }
}
