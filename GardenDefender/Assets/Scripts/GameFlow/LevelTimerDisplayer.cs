using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimerDisplayer : MonoBehaviour
{
    //config params
    RawImage rawImage;
    [SerializeField] Image timerSlider;
    [SerializeField] float speedOfSliderAnim = 0.1f;
    [SerializeField] float levelTimePeriod = 10f;
    [SerializeField] GameController GameController;
    float timerWidth;
    bool isTimerfinished;
    bool isGameStarted;

    //Cashed reference
    LevelTimer levelTimer;
    Rect uvRect;
    RectTransform handler;

    private void Awake()
    {
        levelTimer = new LevelTimer();

        rawImage = transform.Find("Timer Slider").Find("Timer Indicator").GetComponent<RawImage>();
        handler = transform.Find("Handler").GetComponent<RectTransform>();
    }

    private void Start()
    {
        levelTimePeriod = GameController.maxTimer;
        levelTimer.SetAndGetEndTime(levelTimePeriod);
        timerSlider.fillAmount = levelTimer.GetTimerNormalized(0);
        timerWidth = timerSlider.rectTransform.rect.width;
        isGameStarted = true;
    }

    private void Update()
    {
        if (!isTimerfinished)
        {
            MoveSlider(levelTimer.CountLevelTimer());
        }
    }

    private void MoveSlider(float currentTime)
    {
        float time = levelTimer.GetTimerNormalized(currentTime);
        timerSlider.fillAmount = time;

        uvRect = rawImage.uvRect;
        uvRect.x -= speedOfSliderAnim * Time.deltaTime;
        rawImage.uvRect = uvRect;

        handler.anchoredPosition = new Vector2(time * (-timerWidth), 0);

        CheckIfTimerFinished(currentTime);
    }

    private void CheckIfTimerFinished(float currentTime)
    {
        if (currentTime >= levelTimePeriod && isGameStarted)
        {
            //Debug.Log("Almost there!");
            isTimerfinished = true;
            GameController.isTimerStopped = true;
            GameController.IsLevelComplete();
        }
    }
}
