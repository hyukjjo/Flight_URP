using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewStageButton : UIViewBase
{
    public Text textStageNumber;
    public GameObject[] stars;

    private int starCount = 0;

    public override void InitView()
    {
        if (textStageNumber == null)
            textStageNumber = GetComponentInChildren<Text>(true);

    }

    public override void ResetView()
    {

    }

    public void SetStageData(int index)
    {
        textStageNumber.text = DataManager.Instance.stageClass.listStage[index].stageName;
        starCount = DataManager.Instance.GetStageData("Star", index);

        for(int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
