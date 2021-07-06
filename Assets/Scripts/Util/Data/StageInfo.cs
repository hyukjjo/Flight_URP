using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    private static StageInfo instance = null;

    public string stageName;
    public float speedIncrement;
    public int figureCount;
    public int comboMissionCount;
    public int colorMissionCount;
    public int shapeMissionCount;
    public int goldReward;
    public int bestScore;
    public int comboMission;
    public int colorMission;
    public int shapeMission;


    public static StageInfo Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<StageInfo>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SetStageInfo(int index)
    {
        sStage stage = DataManager.Instance.stageClass.listStage[index];
        sStageData stageData = DataManager.Instance.GetStageData(index);

        stageName = stage.stageName;
        speedIncrement = stage.speedIncrement;
        figureCount = stage.figureCount;
        comboMissionCount = stage.comboMission;
        colorMissionCount = stage.colorMission;
        shapeMissionCount = stage.shapeMission;
        goldReward = stage.goldReward;
        bestScore = stageData.stageScore;
        comboMission = stageData.stageComboMission;
        colorMission = stageData.stageColorMission;
        shapeMission = stageData.stageShapeMission;
    }
}