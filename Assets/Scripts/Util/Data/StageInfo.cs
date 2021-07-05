using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    private static StageInfo instance = null;

    public string stageName;
    public float speedIncrement;
    public int figureCount;
    public int comboMission;
    public int colorMission;
    public int shapeMission;
    public int goldReward;
    public int bestScore;


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
}

