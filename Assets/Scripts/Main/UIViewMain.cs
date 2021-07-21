using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIViewMain : UIViewBase
{
    public Text textPlayerGold;

    public override void InitView()
    {
        MainManager.SetMainState(EMainState.MAIN);
        Timer.InitTimer();
        textPlayerGold.text = DataManager.Instance.GetPlayerData().playerGold.ToString();
    }

    public override void ResetView()
    {
        
    }
}