using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIViewMain : UIViewBase
{
    private int playerGold = 0;
    private int playerCurrentLife = 20;
    private readonly int playerMaxLife = 20;

    public Text textPlayerGold;
    public Text textLife;
    public Text textLifeRewardTime;

    public override void InitView()
    {
        if(PlayerPrefs.HasKey("PlayerGold"))
        {
            playerGold = PlayerPrefs.GetInt("PlayerGold");
            textPlayerGold.text = playerGold.ToString();
        }
        else
        {
            textPlayerGold.text = playerGold.ToString();
        }

        if(PlayerPrefs.HasKey("PlayerCurrentLife"))
        {
            playerCurrentLife = PlayerPrefs.GetInt("PlayerCurrentLife");
            textLife.text = playerCurrentLife.ToString() + " / " + playerMaxLife.ToString();
        }
        else
        {
            textLife.text = playerCurrentLife.ToString() + " / " + playerMaxLife.ToString();
        }
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
