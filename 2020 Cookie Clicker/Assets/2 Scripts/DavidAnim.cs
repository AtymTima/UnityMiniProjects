using UnityEngine;
using System;

public class DavidAnim : MonoBehaviour
{
    [Header("Cashed References")]
    [SerializeField] private Animator davidAnim;
    [SerializeField] private SpriteRenderer davidSpRenderer;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Parameters")]
    [SerializeField] private string isClicked = "isClicked";
    private int frame;
    private Color32 currentColor;
    private Color32 originalColor = new Color32(85, 200, 85, 255);

    private void Awake()
    {
        PlayerInteractions.OnCookieClicked += OnClick;
        Spawner.OnAutoSpawn += OnClick;
        davidSpRenderer.color = originalColor;
        currentColor = originalColor;
    }

    private void OnDestroy()
    {
        PlayerInteractions.OnCookieClicked -= OnClick;
        Spawner.OnAutoSpawn -= OnClick;
    }

    private void OnClick()
    {
        davidAnim.SetTrigger(isClicked);
    }

    private void AdjustColors()
    {
        frame++;
        if (frame % 2 == 0)
        {
            if (scoreManager.currentScore <= 10000)
            {
                currentColor.r = (byte)(85 + 115 * scoreManager.currentScore / 10000);
                davidSpRenderer.color = currentColor;
            }
            else if (scoreManager.currentScore <= 100000)
            {
                currentColor.r = 200;
                currentColor.g = (byte)(200 - 115 * scoreManager.currentScore / 100000);
                davidSpRenderer.color = currentColor;
            }
            else if (scoreManager.currentScore < 1000000)
            {
                currentColor.r = 200;
                currentColor.g = 85;
                currentColor.g = (byte)(85 + 115 * scoreManager.currentScore / 1000000);
                davidSpRenderer.color = currentColor;
            }
            else
            {

            }
        }
    }
}
