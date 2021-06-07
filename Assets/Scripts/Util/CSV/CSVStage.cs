using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct sStage
{
    public int index;
    public string stage;
    public string stageName;
    public float speedIncrement;
    public int figureCount;
    public int comboMission;
    public int colorMission;
    public int shapeMission;
    public int goldReward;
}

[Serializable]
public class CSVStage : CSVClass<sStage>
{
    public List<sStage> listStage = new List<sStage>();

    public override bool LoadCSV(string file)
    {
        if (dataDic.Count > 0)
        {
            Debug.Log("CSV Stage had loaded already : " + file);
            return false;
        }

        List<Dictionary<string, object>> data = CSVReader.Read(file);

        for (int i = 0; i < data.Count; i++)
        {
            sStage ss = new sStage
            {
                index = int.Parse(data[i]["Index"].ToString()),
                stage = data[i]["Stage"].ToString(),
                stageName = data[i]["StageName"].ToString(),
                speedIncrement = float.Parse(data[i]["SpeedIncrement"].ToString()),
                figureCount = int.Parse(data[i]["FigureCount"].ToString()),
                comboMission = int.Parse(data[i]["ComboMission"].ToString()),
                colorMission = int.Parse(data[i]["ColorMission"].ToString()),
                shapeMission = int.Parse(data[i]["ShapeMission"].ToString()),
                goldReward = int.Parse(data[i]["GoldReward"].ToString())
            };
            dataDic.Add(ss.index, ss);
            listStage.Add(ss);
        }
        Debug.Log("Load Stage Class : " + file);

        return true;
    }
}