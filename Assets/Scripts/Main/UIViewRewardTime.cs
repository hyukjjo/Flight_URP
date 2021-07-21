using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewRewardTime : UIViewBase
{
    public Text textRewardTime;

    public override void InitView()
    {
        Timer.StartTimer();
    }

    public override void ResetView()
    {
        Timer.StopTimer();
    }

    private void Update()
    {
        string minutes = ((int)(600 - Timer.GetTime()) / 60).ToString("00");
        string seconds = ((int)((600 - Timer.GetTime()) % 60)).ToString("00");
        textRewardTime.text = string.Format("{0} : {1}", minutes, seconds);
    }
}