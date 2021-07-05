using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public string startDate = string.Empty;
    public string exitDate = string.Empty;
    public bool isTutorialDone = false;
    public int playerGold = 0;
    public int playerLife = 20;
}

public class StageData
{
    public List<sStageData> stageDataList = new List<sStageData>();
}

[System.Serializable]
public struct sStageData
{
    public int stageScore;
    public int stageStar;
}

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;
    private PlayerData playerData = null;
    private StageData stageData = null;
    

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public sStageData GetStageData(int index)
    {
        return stageData.stageDataList[index];
    }

    public void SetPlayerData(string name, object value)
    {
        switch(name)
        {
            case "StartDate":
                break;
            case "ExitDate":
                break;
            case "Tutorial":
                playerData.isTutorialDone = (bool)value;
                break;
            case "PlayerGold":
                playerData.playerGold = (int)value;
                break;
            case "PlayerLife":
                playerData.playerLife = (int)value;
                break;
            default:
                break;
        }

    }

    public void SetStageData(int index, int score, int star)
    {
        stageData.stageDataList[index] = new sStageData { stageScore = score, stageStar = star };
    }

    public void ExportPlayerData(object data)
    {
        string jsonString = JsonUtility.ToJson(data);
        FileStream fileStream = new FileStream("Assets/Resources/Data/PlayerData.json", FileMode.Create);
        StreamWriter streamWriter = new StreamWriter(fileStream);
        streamWriter.Write(jsonString);
        streamWriter.Close();
    }

    public void ExportStageData(object data)
    {
        string jsonString = JsonUtility.ToJson(data);
        FileStream fileStream = new FileStream("Assets/Resources/Data/StageData.json", FileMode.Create);
        StreamWriter streamWriter = new StreamWriter(fileStream);
        streamWriter.Write(jsonString);
        streamWriter.Close();
    }

    public PlayerData ReadPlayerData()
    {
        FileStream fileStream = new FileStream("Assets/Resources/Data/PlayerData.json", FileMode.Open);
        StreamReader streamReader = new StreamReader(fileStream);
        string jsonString = streamReader.ReadToEnd();
        return JsonUtility.FromJson<PlayerData>(jsonString);
    }

    public StageData ReadStageData()
    {
        FileStream fileStream = new FileStream("Assets/Resources/Data/StageData.json", FileMode.Open);
        StreamReader streamReader = new StreamReader(fileStream);
        string jsonString = streamReader.ReadToEnd();
        return JsonUtility.FromJson<StageData>(jsonString);
    }

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        DontDestroyOnLoad(gameObject);

        LoadCSV();
        LoadData();
    }

    private void LoadData()
    {
        playerData = ReadPlayerData();
        stageData = ReadStageData();
    }

    public void SaveData()
    {
        if(playerData != null)
            ExportPlayerData(playerData);

        if (stageData != null)
            ExportStageData(stageData);
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

    private void OnApplicationQuit()
    {
        SaveData();
    }
}
