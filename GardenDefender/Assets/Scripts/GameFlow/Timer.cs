using UnityEngine;
using System;
using System.Collections;

public class Timer : MonoBehaviour
{

    private Action timerCallback;
    private float timerRequired;
    private bool isTimerFinished;

    #region Timer
    //public Timer(Action timerCallback, float timerRequired)
    //{
    //    this.timerCallback = timerCallback;
    //    this.timerRequired = timerRequired;
    //    isTimerFinished = false;
    //}

    //private void Update()
    //{
    //    if (!isTimerFinished)
    //    {
    //        timerRequired -= Time.deltaTime;
    //        if (timerRequired < 0f)
    //        {
    //            Debug.Log(timerRequired);
    //            timerCallback();
    //            DestroySelf();
    //        }
    //    }
    //}

    //private void DestroySelf()
    //{
    //    isTimerFinished = true;
    //}
    #endregion

    public IEnumerator SetTimer(Action timerCallback, float timerRequired)
    {
        this.timerCallback = timerCallback;
        yield return new WaitForSeconds(timerRequired);
        timerCallback();
    }
}
