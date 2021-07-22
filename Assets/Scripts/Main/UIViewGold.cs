using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewGold : UIViewBase
{
    public Text textPlayerGold;

    public override void InitView()
    {
        textPlayerGold.text = DataManager.Instance.GetPlayerData().playerGold.ToString();
    }

    public override void ResetView()
    {

    }
}
