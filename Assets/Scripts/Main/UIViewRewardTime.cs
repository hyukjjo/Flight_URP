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
        textRewardTime.text = ((int)(600 - Timer.GetTime()) / 60).ToString() + " : " + ((int)((600 - Timer.GetTime()) % 60)).ToString();
    }
}