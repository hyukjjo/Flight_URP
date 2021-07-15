using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIViewLife : UIViewBase
{

    public Text textLife;

    private readonly int playerMaxLife = 20;
    private int playerLife;

    public override void InitView()
    {
        DataManager.Instance.OnChangedPlayerData += InitView;
        playerLife = DataManager.Instance.GetPlayerData().playerLife;

        if (playerLife < 20)
        {
            UIManager.GetInstance.ActiveView("UIViewRewardTime");
        }
        else
        {
            UIManager.GetInstance.HideView("UIViewRewardTime");
        }

        textLife.text = playerLife.ToString() + " / " + playerMaxLife.ToString();
    }

    public override void ResetView()
    {
        
    }
}
