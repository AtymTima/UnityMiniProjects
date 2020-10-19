using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer
{
    //config params
    float currentTime;
    float startTime;
    float endTime = 30f;

    public float CountLevelTimer()
    {
        currentTime += Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0, endTime);
        return currentTime;
    }

    public float GetTimerNormalized(float time)
    {
        return time / endTime;
    }

    public float SetAndGetEndTime(float endingTime)
    {
        endTime = endingTime;
        return endTime;
    }
}
