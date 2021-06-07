using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSVDataManager : MonoBehaviour
{
    private void Awake()
    {
        LoadCSV();
    }

    public string file_Stage = "FlightTable_Stage";
    private bool isCSVLoaded = false;

    [HideInInspector]
    public CSVStage stageClass = new CSVStage();

    public void LoadCSV()
    {
        if (isCSVLoaded)
        {
            return;
        }
        else
        {
            if (!string.IsNullOrEmpty(file_Stage))
                stageClass.LoadCSV(file_Stage);

            isCSVLoaded = true;
        }
    }
}
