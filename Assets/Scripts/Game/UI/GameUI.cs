using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text txtStage;
    public Text txtBestScore;
    public Text txtScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        txtStage.text = StageInfo.Instance.stageName;
        txtBestScore.text = string.Format("{0:n0}", StageInfo.Instance.bestScore);
    }
}
