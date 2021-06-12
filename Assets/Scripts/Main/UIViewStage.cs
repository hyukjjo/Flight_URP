using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewStage : UIViewBase
{
    public UIViewStageButton[] stageButtons;

    public void SetStage(int stageNumber)
    {
        if(stageNumber == 1)
        {
            for(int i = 0; i < stageButtons.Length; i++)
            {
                stageButtons[i].SetStageData(i);
            }
        }
        else if(stageNumber == 2)
        {
            for (int i = 0; i < stageButtons.Length; i++)
            {
                stageButtons[i].SetStageData(i + 10);
            }
        }
        else if(stageNumber == 3)
        {
            for (int i = 0; i < stageButtons.Length; i++)
            {
                stageButtons[i].SetStageData(i + 20);
            }
        }
    }

    public override void InitView()
    {
        SetStage(1);
    }

    public override void ResetView()
    {
        throw new System.NotImplementedException();
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
