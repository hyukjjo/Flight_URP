using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewShop : UIViewBase
{
    public Text textPlayerGold;

    public override void InitView()
    {
        textPlayerGold.text = DataManager.Instance.GetPlayerData().playerGold.ToString();
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
