using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewStageButton : UIViewBase
{
    public Text textStageNumber;
    public GameObject[] stars;

    public override void InitView()
    {
        if (textStageNumber == null)
            textStageNumber = GetComponentInChildren<Text>();
    }

    public override void ResetView()
    {

    }

    public void SetStageData(int index)
    {
        textStageNumber.text = DataManager.Instance.stageClass.listStage[index].stageName;
    }
}
