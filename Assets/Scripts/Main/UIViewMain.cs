using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIViewMain : UIViewBase
{
    private readonly int playerMaxLife = 20;

    public Text textPlayerGold;
    public Text textLife;
    public Text textLifeRewardTime;

    public override void InitView()
    {
        textPlayerGold.text = DataManager.Instance.GetPlayerData().playerGold.ToString();
        textLife.text = DataManager.Instance.GetPlayerData().playerLife.ToString() + " / " + playerMaxLife.ToString();
    }

    public override void ResetView()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}