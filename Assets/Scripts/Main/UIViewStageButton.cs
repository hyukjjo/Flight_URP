using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewStageButton : UIViewBase
{
    public Text textStageNumber;
    public Text textScore;
    public GameObject[] stars;

    private int starCount = 0;

    public override void InitView()
    {

    }

    public override void ResetView()
    {

    }

    public void SetStageData(int index)
    {
        textStageNumber.text = DataManager.Instance.stageClass.listStage[index].stageName;

        if (textScore != null)
        {
            textScore.text = DataManager.Instance.GetStageData(index).stageScore.ToString();
        }

        starCount = 
            DataManager.Instance.GetStageData(index).stageComboMission + 
            DataManager.Instance.GetStageData(index).stageColorMission + 
            DataManager.Instance.GetStageData(index).stageShapeMission;

        for(int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
        }
    }
}