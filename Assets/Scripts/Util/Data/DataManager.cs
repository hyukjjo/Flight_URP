using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public string startDateTime = string.Empty;
    public string exitDateTime = string.Empty;
    public bool isTutorialDone = false;
    public int playerGold = 0;
    public int playerLife = 20;
    public List<int> stageScore = new List<int>(30);
    public List<int> stageStar = new List<int>(30);
}

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;
    private PlayerData playerData = null;
    private Dictionary<string, List<int>> stageData = new Dictionary<string, List<int>>();

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public int GetStageData(string key, int index)
    {
        List<int> value;
        stageData.TryGetValue(key, out value);
        return value[index];
    }

    public void ExportPlayerData(object data)
    {
        string jsonString = JsonUtility.ToJson(data);
        FileStream fileStream = new FileStream("Assets/Resources/Data/PlayerData.json", FileMode.Create);
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
        LoadPlayerData();
    }



    private void LoadPlayerData()
    {
        playerData = ReadPlayerData();
        stageData.Add("Score", playerData.stageScore);
        stageData.Add("Star", playerData.stageStar);
    }

    public void SavePlayerData()
    {
        if(playerData != null)
            ExportPlayerData(playerData);
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
        SavePlayerData();
    }
}
